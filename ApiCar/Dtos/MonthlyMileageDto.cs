namespace ApiCar.Dtos;

public record MonthlyMileageDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Mileage { get; set; }
}