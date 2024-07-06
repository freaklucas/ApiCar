using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCar.Models;
/// <summary>
/// Prop navegation
/// </summary>
public class User
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    public string? Name { get;}
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Email { get; set; }

    public ICollection<Car>? Cars { get; set; } = new Collection<Car>();
}   