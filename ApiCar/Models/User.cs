using System.ComponentModel.DataAnnotations;

namespace ApiCar.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get;}
    public string? UserName { get; set; }
    public string? Email { get; set; }
}