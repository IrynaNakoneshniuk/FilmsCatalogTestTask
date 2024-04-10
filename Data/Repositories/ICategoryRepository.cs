using FilmsCatalogTestTask.Data.Models;

namespace FilmsCatalogTestTask.Data.Repositories
{
    public interface ICategoryRepository
    { 
        Task<Category> CreateAsync(Category newObject);
        Task DeleteAsync(Category deleteObject);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int? id);
        Task<Category> UpdateAsync(Category updateObject);
        Task<int> CountParentCategoriesAsync(Category category);
        Task<int> CountParentCategoriesRecursiveAsync(Category category);
    }
}