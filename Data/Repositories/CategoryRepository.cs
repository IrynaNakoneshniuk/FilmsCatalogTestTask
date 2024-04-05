
using FilmsCatalogTestTask.Data.ModelsDB;

namespace FilmsCatalogTestTask.Data.Repositories
{
    public class CategoryRepository : IDataAccess
    {
        private readonly FilmsCatalogContext _context;

        public CategoryRepository(FilmsCatalogContext context)
        {
           _context = context;
        }
        public Task<object> Create(Object newObject)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object deleteObject)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<object>> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Update(object updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
