using EduBrain.Entities.Common;
using EduBrain.Entities.Users;

namespace Wasla.Entities;

public class Review : AuditableEntity
{
    public int Id { get; set; } 

    public int? TaskId { get; set; }

    public int? SessionId { get; set; }

    public int ReviewerId { get; set; }

    public int RevieweeId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }


    // Navigation properties
    public Task? Task { get; set; }
    public Session? Session { get; set; }
    public ApplicationUser Reviewer { get; set; } = default!;
    public ApplicationUser Reviewee { get; set; } = default!;
}
