using FilmsCatalogTestTask.Data.Models;

namespace FilmsCatalogTestTask.Data.Repositories
{
    public interface IFilmRepository
    {
        Task<Film> CreateAsync(Film? newObject, List<int>? categories = null);
        Task DeleteAsync(Film ?deleteObject);
        IQueryable<Film> GetAllQueryable();
        Task<Film> GetByIdAsync(int ?id);
        Task<Film> UpdateAsync(Film ?updateObject);
    }
}