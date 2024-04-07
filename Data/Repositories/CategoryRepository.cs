using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.ModelsDB;

namespace FilmsCatalogTestTask.Data.Repositories
{
    public class CategoryRepository 
    {
        private readonly FilmsCatalogContext _context;

        public CategoryRepository(FilmsCatalogContext context)
        {
           _context = context;
        }

        public Task<Category> Create(Category newObject)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Category deleteObject)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetPageOfFilms(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Category> Update(Category updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
