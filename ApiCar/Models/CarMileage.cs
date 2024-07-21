using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCar.Models;

public class CarMileage
{
    [Key] public int Id { get; set; }
    [Required] public int CarId { get; set; }
    [Required] public int Mileage { get; set; }
    [Required] public DateTime Date { get; set; }
    [NotMapped] public virtual Car? Car { get; set; }
}