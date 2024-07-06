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
    [Required]
    public string? Make { get; set; }
    [Required]
    public string? Model { get; set; }
    [Required]
    public TransmissionType Transmission { get; set; }
    [Required]
    [Range(1800, 2025, ErrorMessage = "Intervalo inválido de ano.")]
    public DateTime Year { get; set; }
    [Required]
    [Range(500, double.MaxValue, ErrorMessage = "Preço precisa ser um valor positivo.")]
    public decimal Price { get; set; }
    [Required]
    public int UserId { get; set; }
    public virtual User? Owner { get; set; }
}