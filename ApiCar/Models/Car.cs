using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCar.Enums;

namespace ApiCar.Models;

/// <summary>
/// Prop navegation
/// </summary>

public class Car
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public TransmissionType Transmission { get; set; }
    public DateTime Year { get; set; }
    public decimal Price { get; set; }
    
    public int UserId { get; set; }
    public virtual User? Owner { get; set; }
}