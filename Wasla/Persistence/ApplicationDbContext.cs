using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using System.Security.Claims;
namespace Wasla.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) :
    IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    // identity-related entities
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Seeker> Seekers => Set<Seeker>();
    public DbSet<Helper> Helpers => Set<Helper>();

    // AI entities
    public DbSet<AIUsage> AIUsages => Set<AIUsage>();

    // Communication entities
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Session> Sessions => Set<Session>();

    // Financial entities
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<WalletTransaction> WalletTransactions => Set<WalletTransaction>();

    // Gamification entities
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<PointTransaction> PointTransactions => Set<PointTransaction>();
    public DbSet<UserBadge> UserBadges => Set<UserBadge>();

    // Marketplace entities
    public DbSet<SeekerTask> Tasks => Set<SeekerTask>();
    public DbSet<TaskOffer> TaskOffers => Set<TaskOffer>();
    public DbSet<TaskAttachment> TaskAttachments => Set<TaskAttachment>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectOffer> ProjectOffers => Set<ProjectOffer>();
    public DbSet<ProjectMilestone> ProjectMilestones => Set<ProjectMilestone>();
    public DbSet<MilestoneDeliverable> MilestoneDeliverables => Set<MilestoneDeliverable>();
    public DbSet<ProjectAttachment> ProjectAttachments => Set<ProjectAttachment>();

    // Profile entities
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<HelperSkill> HelperSkills => Set<HelperSkill>();
    public DbSet<HelperService> HelperServices => Set<HelperService>();
    public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();
    public DbSet<HelperProject> HelperProjects => Set<HelperProject>();
    public DbSet<ProjectSkill> ProjectSkills => Set<ProjectSkill>();

    // Trust & Safety entities
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<VerificationRequest> VerificationRequests => Set<VerificationRequest>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(w =>
            w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Automatically apply all configurations from the current assembly that implement IEntityTypeConfiguration<T>

        // Configure CreatedBy and UpdatedBy relationships for all AuditableEntity types
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(AuditableEntity).IsAssignableFrom(e.ClrType)))
        {
            var builder = modelBuilder.Entity(entityType.ClrType);

            builder.HasOne(typeof(ApplicationUser), "CreatedBy")
                .WithMany()
                .HasForeignKey("CreatedById")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(ApplicationUser), "UpdatedBy")
                .WithMany()
                .HasForeignKey("UpdatedById")
                .OnDelete(DeleteBehavior.Restrict);
        }

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entityEntry in entries)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(x => x.CreatedById).CurrentValue = currentUserId;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                entityEntry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
