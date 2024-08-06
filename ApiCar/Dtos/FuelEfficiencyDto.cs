namespace ApiCar.Dtos;

public class FuelEfficiencyDto
{
    public int CarId { get; set; }
    public string? CarModel { get; set; }
    public decimal TotalFuelConsumed { get; set; }
    public int TotalDistanceTraveled { get; set; }
    public decimal FuelEfficiency { get; set; }
}