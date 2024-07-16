using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCar.Models;

public class InsurancePolicy
{
    [Key] public int Id { get; set; }

    [Required] public string ProviderName { get; set; }

    [Required] public DateTime StartDate { get; set; }

    [Required] public DateTime EndDate { get; set; }

    [Required] public decimal Premium { get; set; }

    [Required] public int CarId { get; set; }

    [NotMapped] public virtual Car? Car { get; set; }
}