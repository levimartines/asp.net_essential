using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Filters;
using MyWebApplication.Models;
using MyWebApplication.Repositories;

namespace MyWebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger) : ControllerBase
{
    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> Get()
    {
        logger.LogInformation("#######");
        var categories = repository.GetAll();
        return Ok(categories);
    }

    [HttpGet("{id:int:min(1)}", Name = "Categories/GetById")]
    public ActionResult<Category> GetById(int id)
    {
        var category = repository.Get(id);
        if (category is null)
            return NotFound($"Category with id={id} not found");

        return category;
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        var created = repository.Create(category);
        return new CreatedAtRouteResult("Categories/GetById", new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();

        var updated = repository.Update(category);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = repository.Get(id);
        if (category is null)
            return NotFound();
        
        repository.Delete(id);
        return NoContent();
    }
    
}