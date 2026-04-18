using System.ComponentModel.DataAnnotations;

namespace Wasla.Common.Authentication;

/*
 *  implement options pattern
    this class is used to bind the configuration from appsettings.json 
    
    Conditions for options class: 
        -Must be non-abstract.
        -Has public read-write properties of the type that have corresponding items in config are bound.


     --> Read about interface IOptions<T> 
 */
public class JwtOptions
{
    public static string SectionName = "Jwt"; // it must match the section name in appsettings.json 

    [Required]
    public string Key { get; init; } = string.Empty;

    [Required]
    public string Issuer { get; init; } = string.Empty;

    [Required]
    public string Audience { get; init; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int ExpiryMinutes { get; init; }
}