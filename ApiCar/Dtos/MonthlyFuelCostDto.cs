namespace ApiCar.Dtos;

public class MonthlyFuelCostDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalCost { get; set; }
}