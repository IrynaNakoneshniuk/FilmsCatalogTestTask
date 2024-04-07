using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.ModelsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Common;

namespace FilmsCatalogTestTask.Data.Repositories;

public class FilmRepository : IRepository<Film>
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
            EntityEntry<Film> entityEntry = await _context.Films.AddAsync(newObject);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex,"An error occurred while saving to the database.");

            throw;
        }
    }


    public async Task Delete(Film deleteObject)
    {
        try
        {
            _context.Films.Remove(deleteObject);
            await _context.SaveChangesAsync();

        }catch(DbUpdateException ex) {

           _logger.LogError(ex, "An error occurred while removing from database.");
        }
    }


    public async Task<Film> Update(Film updateObject)
    {
        try
        {
            EntityEntry<Film> entityEntry = _context.Films.Update(updateObject);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
        catch( DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while updating to the database.");

            throw;
        }
    }

 
    public async Task<Film> GetById(int id)
    {
        try
        {
            Film? film = await _context.Films
                .Include(film => film.FilmCategories)
                .ThenInclude(filmCategory => filmCategory.Category)
                .Where(film=> film.Id == id)
                .FirstOrDefaultAsync();

            if (film == null)
            {
                throw new InvalidOperationException($"Film with id = {id} not found");
            }
            return film;

        }
        catch(DbException ex)
        {
            _logger.LogError(ex, "An error occurred while searching film by id.");

            throw;
        }
    }

    public async Task<IQueryable<Film>> GetAll()
    {
        try
        {
            return  _context.Films
                .Include(film => film.FilmCategories)
                .ThenInclude(filmCategory => filmCategory.Category);

        }catch(DbException ex)
        {
            _logger.LogError(ex, "An error occurred while loading full list of films");

            throw;
        }
    }
}
