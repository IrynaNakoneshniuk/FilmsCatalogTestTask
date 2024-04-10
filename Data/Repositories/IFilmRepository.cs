using FilmsCatalogTestTask.Data.Models;

namespace FilmsCatalogTestTask.Data.Repositories
{
    public interface IFilmRepository
    {
        Task<Film> CreateAsync(Film? newObject, List<Category>? categories = null);
        Task DeleteAsync(Film ?deleteObject);
        IQueryable<Film> GetAllAsync();
        Task<Film> GetByIdAsync(int ?id);
        Task<Film> UpdateAsync(Film ?updateObject);
    }
}