using System.ComponentModel.DataAnnotations;
using Wasla.Entities.Users;

namespace Wasla.Entities;

public class HelperSkill
{
    public int Id { get; set; } 

    public int HelperId { get; set; }

    public string SkillName { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty; // e.g. "Frontend", "Backend", "Math", "Design"

    // Navigation properties
    public Helper Helper { get; set; } = default!;
}
