using MyWebApplication.Models;

namespace MyWebApplication.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category Get(int id);
    Category Create(Category category);
    Category Update(Category category);
    Category Delete(int id);

}