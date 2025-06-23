using Microsoft.EntityFrameworkCore;
using MyWebApplication.Context;
using MyWebApplication.Controllers;
using MyWebApplication.Models;

namespace MyWebApplicationxUnitTests.UnitTests;

public class ProductsUnitTestController
{
    public static DbContextOptions<AppDbContext> DbContextOptions { get; }

    static ProductsUnitTestController()
    {
        DbContextOptions =  new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;
    }
    
    private AppDbContext GetInMemoryDbContext()
    {
        var context = new AppDbContext(DbContextOptions);
        context.Categories.Add(new Category { Id = 1, Name = "Electronics", ImageUrl = "electronics.jpeg"});
        context.Categories.Add(new Category { Id = 2, Name = "Clothes",  ImageUrl = "clothes.jpeg" });
        context.SaveChanges();
        return context;
    }

    [Fact]
    public void Get_DeveRetornarTodasCategorias()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new CategoriesController(context);

        // Act
        var result = controller.Get();

        // Assert
        var categorias = Assert.IsAssignableFrom<IEnumerable<Category>>(result.Value);
        Assert.Equal(2, categorias.Count());
    }
    
}