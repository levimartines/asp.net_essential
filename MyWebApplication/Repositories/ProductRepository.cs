using Microsoft.EntityFrameworkCore;
using MyWebApplication.Context;
using MyWebApplication.Models;

namespace MyWebApplication.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public IQueryable<Product> GetAll()
    {
        return context.Products;
    }

    public Product Get(int id)
    {
        return context.Products.Find(id);
    }

    public Product Create(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
        return product;
    }

    public bool Update(Product product)
    {
        if (product is null)
        {
            throw new InvalidOperationException("Product is null");
        }

        if (context.Products.Any(p => p.Id == product.Id))
        {
            context.Products.Update(product);
            context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        var product = context.Products.Find(id);
        if (product is not null)
        {
            context.Remove(product);
            context.SaveChanges();
            return true;
        }

        return false;
    }
}