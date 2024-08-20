using System.ComponentModel.DataAnnotations;

namespace ApiCar.Models;

public class CarListing
{
    [Key] public int Id { get; set; }
    [Required] public int CarId { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public DateTime ListingDate { get; set; }
    public string? Description { get; set; }
    public virtual Car Car { get; set; }
}