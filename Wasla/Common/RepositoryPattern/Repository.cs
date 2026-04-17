using EduBrain.Persistence;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EduBrain.Common.RepositoryPattern;

public class Repository<Entity>(ApplicationDbContext context) : IRepository<Entity> where Entity : class
{
    private readonly DbSet<Entity> _dbSet = context.Set<Entity>();
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Entity entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct)
    {
        return await FindAll(predicate).AnyAsync(ct);
    }

    public async Task<int> CountAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct)
    {
        return await FindAll(predicate).CountAsync(ct);
    }

    public async Task Delete(Entity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task UpdateAsync(Entity entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
    }
    public IQueryable<Entity> FindAll(Expression<Func<Entity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task<Entity?> FindAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate, ct);
    }

    public IQueryable<Entity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<Entity?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _dbSet.FindAsync(id, ct);
    }

    public Task AddRangeAsync(IEnumerable<Entity> entities, CancellationToken ct)
    {
        //foreach (var entity in entities)
        //{
        //    entity.CreatedOn = DateTime.UtcNow;
        //}
        return _dbSet.AddRangeAsync(entities, ct);
    }

    // Bulk delete method
    public async Task BulkDeleteWhereAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct)
    {
        await _dbSet
            .Where(predicate)
            .ExecuteDeleteAsync(ct);
    }


    // Bulk update method
    public async Task BulkUpdateWhereAsync(
    Expression<Func<Entity, bool>> predicate,
    Expression<Func<SetPropertyCalls<Entity>, SetPropertyCalls<Entity>>> setProperty,
    CancellationToken ct)
    {
        await _dbSet
            .Where(predicate)
            .ExecuteUpdateAsync(setProperty, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }



    public IQueryable<Entity> Include<TProperty>(Expression<Func<Entity, TProperty>> navigationProperty)
    {
        return _dbSet.Include(navigationProperty);
    }
}
