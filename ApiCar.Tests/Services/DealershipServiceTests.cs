using ApiCar.Models;
using ApiCar.Repositories.Interface;
using ApiCar.Services.Impl;
using Moq;
using Xunit;

namespace ApiCar.Tests.Services;

public class DealershipServiceTests
{
    private readonly DealershipService _service;
    private readonly Mock<IDealershipRepository> _dealershipRepositoryMock;

    public DealershipServiceTests()
    {
        _dealershipRepositoryMock = new Mock<IDealershipRepository>();
        _service = new DealershipService(_dealershipRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllDealerships()
    {
        // Arrange
        var dealerships = new List<Dealership>
        {
            new Dealership
            {
                Id = 1,
                Name = "Chevrolet",
                Address = "Rua 01",
                City = "Rio Verde",
                Contact = "000111444",
                CarListings = null
            },
            new Dealership
            {
                Id = 2,
                Name = "Fiat",
                Address = "Rua 02",
                City = "Rio Verde",
                Contact = "000111222",
                CarListings = null
            }
        };

        _dealershipRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(dealerships);

        // Act
        var result = await _service.GetAllDealerships();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, d => d.Name == "Chevrolet");
        Assert.Contains(result, d => d.Name == "Fiat");
    }

    [Fact]
    public async Task CreateDealership_ShouldCallRepository_WithValidDealership()
    {
        // Arrange
        var dealership = new Dealership
        {
            Id = 1,
            Name = "Chevrolet",
            Address = "Rua 01",
            City = "Rio Verde",
            Contact = "000111444",
            CarListings = null
        };

        // Act
        await _service.CreateDealership(dealership);

        // Assert
        _dealershipRepositoryMock.Verify(repo => repo.Create(dealership), Times.Once);
    }
}
