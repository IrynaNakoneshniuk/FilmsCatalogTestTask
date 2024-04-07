namespace FilmsCatalogTestTask.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Create(T newObject);
        Task<T> Update(T updateObject);
        Task Delete(T deleteObject);
        Task<T> GetById(int id);
        Task<IQueryable<T>> GetAll();
    }
}
