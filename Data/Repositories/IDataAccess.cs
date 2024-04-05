namespace FilmsCatalogTestTask.Data.Repositories
{
    public interface IDataAccess<T>
    {
        Task<T> Create(T newObject);
        Task<T> Update(T updateObject);
        Task Delete(T deleteObject);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize);
    }
}
