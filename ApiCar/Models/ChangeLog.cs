namespace ApiCar.Models;
/// <summary>
/// Histórico de alterações de entidade.
/// </summary>
public class ChangeLog
{
    public int Id { get; set; }
    public string? EntityName { get; set; }
    public int EntityId { get; set; }
    public string? PropertyName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime ChangeDate { get; set; }
    public string? UserName { get; set; }
}