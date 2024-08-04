namespace ApiCar.Dtos;

// Todo: refact Arguments exceptions => To my exceptions

public class MaintenanceDetailDto
{
    private string _description;
    private decimal _cost;
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("A descrição precisa ser preenchida.");
            }
            
            _description = value;
        }
    }

    public decimal Cost
    {
        get => _cost;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("O custo precisa ser maior que 0");
            }

            _cost = value;
        }
    }

    DateTime _date;

    public DateTime Date
    {
        get => _date;
        set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("A data precisa ser maior que a de hoje.");
            }

            _date = value;
        }
    }
}

public class MaintenanceSummaryDto
{
    public int CarId { get; set; }
    public string? CarModel { get; set; }
    public decimal TotalCost { get; set; }
    public decimal AverageCost { get; set; }
    public ICollection<MaintenanceDetailDto>? MaintenanceDetails { get; set; }
}