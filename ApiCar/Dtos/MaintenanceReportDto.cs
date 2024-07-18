namespace ApiCar.Dtos;

public class MaintenanceReportDto
{
    public int CarId { get; set; }
    public decimal TotalCost { get; set; }
    public List<YearlyCostDto> AverageCostPerYear { get; set; }
}