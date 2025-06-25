using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Context;
using MyWebApplication.Filters;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(AppDbContext context, ILogger<CategoriesController> logger) : ControllerBase
{
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> Get()
    {
        logger.LogInformation("#######");
        return context.Categories.Include(p => p.Products).AsNoTracking().ToList();
    }

    [HttpGet("{id:int:min(1)}", Name = "Categories/GetById")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        if (category is null)
            return NotFound($"Category with id={id} not found");

        return category;
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
        return new CreatedAtRouteResult("Categories/GetById", new { id = category.Id }, category);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();

        context.Entry(category).State = EntityState.Modified;
        context.SaveChanges();
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = context.Categories.FirstOrDefault(p => p.Id == id);
        if (category is null)
            return NotFound();
        
        context.Categories.Remove(category);
        context.SaveChanges();
        return NoContent();
    }
    
}