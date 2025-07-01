using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MyWebApplication.Context;
using MyWebApplication.Controllers;
using MyWebApplication.Models;
using MyWebApplication.Repositories;

namespace MyWebApplicationxUnitTests.UnitTests;

public class ProductsUnitTestController
{
    private static DbContextOptions<AppDbContext> DbContextOptions { get; }

    static ProductsUnitTestController()
    {
        DbContextOptions =  new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;
    }

    [Fact]
    public void Get_ShouldReturnAllCategoriesWith200()
    {
        var repository = new Mock<ICategoryRepository>();
        IEnumerable<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category1" },
            new Category { Id = 2, Name = "Category2" }
        };
        repository.Setup(repo => repo.GetAll()).Returns(categories);

        var loggerMock = new Mock<ILogger<CategoriesController>>();
        var controller = new CategoriesController(repository.Object, loggerMock.Object);

        var result = controller.Get();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCategories = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
        Assert.Equal(2, returnedCategories.Count());
    }
    
}