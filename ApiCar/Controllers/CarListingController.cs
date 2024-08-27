using ApiCar.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]

public class CarListingController : ControllerBase
{
    private readonly Context _context;
    
    public CarListingController(Context context)
    {
        _context = context;
    }
}