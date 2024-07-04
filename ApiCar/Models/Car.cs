using System.ComponentModel.DataAnnotations;
using ApiCar.Enums;

namespace ApiCar.Models;

public class Car
{
    [Key]
    public int Id { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public TransmissionType Transmission { get; set; }
    public DateTime Year { get; set; }
    public decimal Price { get; set; }
    
    public virtual User? Owner { get; set; }
}