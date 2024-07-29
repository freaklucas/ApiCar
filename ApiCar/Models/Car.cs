using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiCar.Enums;
using ApiCar.Notifications;

namespace ApiCar.Models;

/// <summary>
///     Prop navegation
/// </summary>
public class Car : Notifiable
{
    [Key] public int Id { get; set; }
    public string? Make { get; set; }
    [Required] public string? Model { get; set; }
    [Required] public TransmissionType Transmission { get; set; }
    [Required]
    [Range(1800, 2025, ErrorMessage = "Intervalo inválido de ano.")]
    public int Year { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public int UserId { get; set; }
    public virtual User? Owner { get; set; }

    public virtual ICollection<MaintenanceRecord>? MaintenanceRecords { get; set; }
    public virtual ICollection<InsurancePolicy>? InsurancePolicies { get; set; }
    public virtual ICollection<CarMileage>? CarMileages { get; set; }
    public virtual ICollection<FuelRecord>? FuelRecords { get; set; }
    [NotMapped] public new IReadOnlyCollection<Notification> Notifications => base.Notifications;

    public void Validate()
    {   
        if (string.IsNullOrEmpty(Make))
            AddNotification(nameof(Make), "A marca é obrigatório.");

        if (Price < 100 || Price < 0)
            AddNotification(nameof(Price), "Não tem como um carro custar tão barato.");
    }
}