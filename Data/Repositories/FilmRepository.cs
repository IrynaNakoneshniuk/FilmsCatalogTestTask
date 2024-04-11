using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.ModelsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Common;

namespace FilmsCatalogTestTask.Data.Repositories;

public class FilmRepository : IFilmRepository
{
    private readonly FilmsCatalogContext _context;
    private readonly ILogger<FilmRepository> _logger;

    public FilmRepository(FilmsCatalogContext context, ILogger<FilmRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<Film> CreateAsync(Film ?newObject, List<int> categories = default)
    {
        try
        {
            if (newObject == null)
            {
                throw new ArgumentNullException(nameof(newObject), "The object passed for film create cannot be null.");
            }

            EntityEntry<Film> filmEntry = await _context.Films.AddAsync(newObject);

            if (categories != null && categories.Count > 0)
            {
                filmEntry.Entity.FilmCategories = new List<FilmCategory>();
                foreach (var category in categories)
                {
                    FilmCategory filmCategory = new FilmCategory
                    {
                        FilmId = filmEntry.Entity.Id,
                        CategoryId = category
                    };

                    filmEntry.Entity.FilmCategories.Add(filmCategory);
                }
            }
            await _context.SaveChangesAsync();

            return filmEntry.Entity;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while saving film to the database.");
            return new Film() { Director = string.Empty, Name = string.Empty };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving film to the database.");
            return new Film() { Director = string.Empty, Name = string.Empty };
        }
    }


    public async Task DeleteAsync(Film? deleteObject)
    {
        try
        {
            if (deleteObject == null)
            {
                throw new ArgumentNullException(nameof(deleteObject), "The object passed for film remove cannot be null.");
            }

            _context.Films.Remove(deleteObject);
            await _context.SaveChangesAsync();

        }
        catch (DbUpdateException ex)
        {

            _logger.LogError(ex, "An error occurred while removing from database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while removing from database.");
        }
    }


    public async Task<Film> UpdateAsync(Film ?updateObject)
    {
        try
        {
            if (updateObject == null)
            {
                throw new ArgumentNullException(nameof(updateObject),
                    "The object passed for film update cannot be null.");
            }

            EntityEntry<Film> entityEntry = _context.Films.Update(updateObject);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while updating to the database.");
            return new Film() { Director = string.Empty, Name = string.Empty };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating to the database.");
            return new Film() { Director = string.Empty, Name = string.Empty };
        }
    }


    public async Task<Film> GetByIdAsync(int ? id)
    {
        try
        {
            Film? film = await _context.Films
                .Include(film => film.FilmCategories)
                .ThenInclude(filmCategory => filmCategory.Category)
                .Where(film => film.Id == id)
                .FirstOrDefaultAsync();

            if (film == null)
            {
                throw new InvalidOperationException($"Film with id = {id} not found");
            }
            return film;
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "An error occurred while searching film by id.");
            return new Film() { Director = string.Empty, Name = string.Empty };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching film by id.");
            return new Film() { Director = string.Empty, Name = string.Empty };
        }
    }


    public IQueryable<Film> GetAllQueryable()
    {
        try
        {
            return _context.Films
                .Include(film => film.FilmCategories)
                .ThenInclude(filmCategory => filmCategory.Category);
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "An error occurred while loading full list of films");
            return new List<Film>().AsQueryable();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while loading full list of films");
            return new List<Film>().AsQueryable();
        }
    }
}
