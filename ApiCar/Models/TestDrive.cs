using System.ComponentModel.DataAnnotations;

namespace ApiCar.Models;

public class TestDrive
{
    [Key] public int Id { get; set; }
    
    [Required] public int CarId { get; set; }
    public virtual Car Car { get; set; }

    [Required] public int DealershipId { get; set; }
    public virtual Dealership Dealership { get; set; }

    [Required] public int UserId { get; set; }
    public virtual User User { get; set; }

    [Required] public DateTime ScheduledDate { get; set; }

    public string? Notes { get; set; }
}
