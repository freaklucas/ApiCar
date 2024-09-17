using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCar.Models;

public class CarListing
{
    [Key] public int Id { get; set; }
    [Required] public int CarId { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public DateTime ListingDate { get; set; }
    public string? Description { get; set; }
    public virtual Car? Car { get; set; }
    public virtual VehicleReport? VehicleReport { get; set; }

    [ForeignKey("DealershipId")]
    public int? DealershipId { get; set; }
    public virtual Dealership? Dealership { get; set; }
}