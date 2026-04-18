namespace Wasla.Entities;

public class HelperServices : AuditableEntity
{
    public int Id { get; set; } 

    public int HelperId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty; // e.g. "Frontend", "Backend", "Math", "Design"

    public int Price { get; set; } 

    // Navigation properties
    public Helper Helper { get; set; } = default!;
}
