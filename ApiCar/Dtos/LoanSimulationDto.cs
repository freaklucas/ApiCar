namespace ApiCar.Dtos;

public class LoanSimulationDto
{
    public decimal CarPrice { get; set; }
    public decimal DownPayement { get; set; }
    public int Months { get; set; }

    public decimal InterestRate => 0.01M;
}