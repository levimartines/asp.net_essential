using Microsoft.EntityFrameworkCore;
using MyWebApplication.Context;
using MyWebApplication.Models;

namespace MyWebApplication.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public IEnumerable<Category> GetAll()
    {
        return context.Categories.ToList();
    }

    public Category Get(int id)
    {
        return context.Categories.Find(id);
    }

    public Category Create(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
        return category;
    }

    public Category Update(Category category)
    {
        if (category is null)
            throw new ArgumentNullException(nameof(category));

        context.Entry(category).State = EntityState.Modified;
        context.SaveChanges();
        return category;
    }

    public Category Delete(int id)
    {
        var category = context.Categories.Find(id);
        if (category is null)
            throw new ArgumentNullException(nameof(category));
        context.Categories.Remove(category);
        context.SaveChanges();
        return category;
    }
}