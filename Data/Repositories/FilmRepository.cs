using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.ModelsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FilmsCatalogTestTask.Data.Repositories;

public class FilmRepository : IDataAccess<Film>
{
    private readonly FilmsCatalogContext _context;
    private readonly ILogger<FilmRepository> _logger;

    public FilmRepository(FilmsCatalogContext context, ILogger<FilmRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Film> Create(Film newObject)
    {
        try
        {
            if (newObject == null)
                throw new ArgumentNullException(nameof(newObject), "New object cannot be null.");

            EntityEntry<Film> entityEntry = await _context.Films.AddAsync(newObject);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while saving changes to the database.");

            return new Film() { Name=string.Empty,Director= string.Empty };
        }
    }


    public async Task Delete(Film deleteObject)
    {
        try
        {
            _context.Films.Remove(deleteObject);
            await _context.SaveChangesAsync();

        }catch(DbUpdateException ex) {

            _logger.LogError(ex, "An error occurred while saving changes to the database.");
        }
    }


    public Task<Film> Update(Film updateObject)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Film>> IDataAccess<Film>.GetAll(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    Task<Film> IDataAccess<Film>.GetById(int id)
    {
        throw new NotImplementedException();
    }
}
