using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;
using MyWebApplication.Repositories;

namespace MyWebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = repository.GetAll();
        if (products is null)
        {
            return NotFound("No products found");
        }

        return products.ToList();
    }

    [HttpGet("{id:int}", Name = "Products/GetById")]
    public ActionResult<Product> GetById(int id)
    {
        var product = repository.Get(id);
        if (product is null)
        {
            return NotFound("Product not found");
        }

        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        var created = repository.Create(product);
        return new CreatedAtRouteResult("Products/GetById", new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        var updated = repository.Update(product);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var deleted = repository.Delete(id);
        if (deleted)
            return NoContent();

        return NotFound();
    }
}