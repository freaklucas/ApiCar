using System.ComponentModel.DataAnnotations;

namespace ApiCar.Models;

public class Dealership
{
    [Key] public int Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? City { get; set; }
    [Required] public string? Address { get; set; }
    [Required] public string? Contact { get; set; }
    
    public virtual ICollection<CarListing>? CarListings { get; set; }
}