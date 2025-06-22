using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Context;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _context.Products;
        if (products is null)
        {
            return NotFound("No products found");
        }
        return products.ToList();
    }

    [HttpGet("{id:int}", Name = "Products/GetById")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            return NotFound("Product not found");
        }
        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return new CreatedAtRouteResult("Products/GetById", new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return NotFound();
        
        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
    
}