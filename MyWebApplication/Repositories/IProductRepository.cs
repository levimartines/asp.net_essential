using MyWebApplication.Models;

namespace MyWebApplication.Repositories;

public interface IProductRepository
{
    IQueryable<Product> GetAll();
    Product Get(int id);
    Product Create(Product product);
    bool Update(Product product);
    bool Delete(int id);
    
}