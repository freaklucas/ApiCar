using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCar.Models;

/// <summary>
/// Funcionalidade: Histórico de Manutenções dos Carros
// Funcionalidade para gerenciar o histórico de manutenções dos carros.
// Isso permitirá que os usuários registrem e consultem manutenções realizadas em cada carro, fornecendo um histórico completo para cada veículo.
/// </summary>
public class MaintenanceRecord
{
    [Key] public int Id { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public string Description { get; set; }
    [Required] public decimal Cost { get; set; }
    [Required] public int CarId { get; set; }
    [NotMapped] public virtual Car? Car { get; set; }
}