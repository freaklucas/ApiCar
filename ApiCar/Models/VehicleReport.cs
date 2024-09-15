using System.ComponentModel.DataAnnotations;
using ApiCar.Enums;

namespace ApiCar.Models;

public class VehicleReport
{
    [Key] public int Id { get; set; }
 
    [Required] public string? NumberReport { get; set; }
    
    [Required] public string? Chassi { get; set; }
    
    [Required] public string? EngineNumber { get; set; }
    
    [Required] public string? LicensePlate { get; set; } // placa
    
    [Required] public string? Make { get; set; }
    
    [Required] public int Mileage { get; set; }
    
    [Required] public string? Color { get; set; }
    
    [Required] public int YearOfManufacture { get; set; }
    
    [Required] public EChassiStatus EChassiStatus { get; set; }
    
    public int CarListingId { get; set; }
    public virtual CarListing? CarListing { get; set; }
}