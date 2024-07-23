using System.ComponentModel.DataAnnotations;

namespace ApiCar.Models;

public class FuelRecord
{
    [Key] public int Id { get; set; }
    [Required] public int CarId { get; set; }
    [Required] public int Quantity { get; set; }
    [Required] public int Price { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public int Mileage { get; set; }
    public virtual Car? Car { get; set; }
}