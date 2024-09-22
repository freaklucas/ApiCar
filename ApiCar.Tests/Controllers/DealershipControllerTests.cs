using ApiCar.Controllers;
using ApiCar.Models;
using ApiCar.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiCar.Tests.Controllers;

public class DealershipControllerTests
{
    private readonly Mock<IDealershipService> _dealershipServiceMock;
    private readonly DealershipController _controller;

    public DealershipControllerTests()
    {
        _dealershipServiceMock = new Mock<IDealershipService>();
        _controller = new DealershipController(_dealershipServiceMock.Object);
    }

    [Fact]
    public async Task GetAllDealerships_ShouldReturnOk_WithListOfDealerships()
    {
        // Arrange
        var dealerships = new List<Dealership>
        {
            new Dealership { Id = 1, Name = "GWM" },
            new Dealership { Id = 2, Name = "BMW" }
        };

        _dealershipServiceMock.Setup(service => service.GetAllDealerships()).ReturnsAsync(dealerships);

        // Act
        var result = await _controller.GetAllDealerships();

        Assert.NotNull(result);
        Assert.Equal(2, dealerships.Count);
    }

    [Fact]
    public async Task AddDealership_ShouldReturnCreatedAtAction()
    {
        // arrange
        var newDealership = new Dealership { Id = 3, Name = "Citroen"};
        _dealershipServiceMock.Setup(service => service.CreateDealership(It.IsAny<Dealership>())).Returns(Task.CompletedTask);

        // act
        var result = await _controller.AddDealership(newDealership);

        // Assert
        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
        var addedDealership = Assert.IsType<Dealership>(createdAtAction.Value);

        Assert.Equal(3, addedDealership.Id);
    }
}
