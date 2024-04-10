using Azure.Core.GeoJson;
using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.ModelsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Common;

namespace FilmsCatalogTestTask.Data.Repositories
{
    public class CategoryRepository :  ICategoryRepository
    {
        private readonly FilmsCatalogContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(FilmsCatalogContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
       
        public async Task<int> CountParentCategoriesAsync(Category category)
        {
            try
            {
                if (category == null)
                    return 0;

                   return await CountParentCategoriesRecursiveAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while counting parent categories.");
                return 0;
            }
        }

        public async Task<int> CountParentCategoriesRecursiveAsync(Category category)
        {
            try
            {
                if (category.ParentCategoryId == null)
                    return 0;

                var parentCategory = await _context.Categories.FindAsync(category.ParentCategoryId);
                if (parentCategory == null)
                    return 0;

                    return 1+  await CountParentCategoriesRecursiveAsync(parentCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while counting parent categories recursively.");
                return 0;
            }
        }

        public async Task<Category> CreateAsync(Category newObject)
        {
            try
            {
                if (newObject == null)
                {
                    _logger.LogError("New object can't be equals null. Object Id: {Id}", newObject);
                    throw new ArgumentNullException(nameof(newObject), "The object passed for category create cannot be null.");
                }

                if (newObject.Id.Equals(newObject.ParentCategoryId)
                    && newObject.ParentCategory.Id.Equals(newObject.Id))
                {
                    _logger.LogError("Parent and new object can't be equals. Object Id: {Id}", newObject.Id);
                    throw new ArgumentException("Parent and new object can`t be equals");
                }

                EntityEntry<Category> entityEntry = await _context.Categories.AddAsync(newObject);
                await _context.SaveChangesAsync();

                return entityEntry.Entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while saving category to the database.");
                return new Category() { Name = string.Empty };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving category to the database.");

                return new Category() { Name = string.Empty };
            }
        }

        public async Task DeleteAsync(Category deleteObject)
        {
            try
            {
                if (deleteObject == null)
                {
                    throw new ArgumentNullException(nameof(deleteObject), "The object passed for category remove cannot be null.");
                }

                _context.Categories.Remove(deleteObject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while delete category from database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while delete category from database.");
            }
        }

        public async Task <IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                var categories = await _context.Categories
                                     .Include(c => c.ParentCategory)
                                     .Include(c => c.FilmCategories)
                                     .ThenInclude(fc => fc.Film)
                                     .ToListAsync();

                foreach (var category in categories)
                {
                    category.NestedLevel = await CountParentCategoriesAsync(category);
                }

                return categories;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while reading category from database.");
                return new List<Category>().AsQueryable();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading category from database.");
                return new List<Category>().AsQueryable();
            }
        }

        public async Task<Category> GetByIdAsync(int? id)
        {
            try
            {
                return await _context.Categories
               .Include(c => c.ParentCategory)
               .Include(c => c.FilmCategories)
               .ThenInclude(c => c.Film)
               .Where(c => c.Id == id)
               .FirstAsync();

            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while reading category by id from database.");
                return new Category() { Name = string.Empty };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while reading category by id from database.");
                return new Category() { Name = string.Empty };
            }
        }

        public async Task<Category> UpdateAsync(Category updateObject)
        {
            try
            {
                if (updateObject.Id.Equals(updateObject.ParentCategoryId)
                    && updateObject.ParentCategory.Id.Equals(updateObject.Id))
                {
                    _logger.LogError("Parent and new object can't be equals. Object Id: {Id}", updateObject.Id);
                    throw new ArgumentException("Parent and new object can`t be equals");
                }

                EntityEntry<Category> categoryEntry = _context.Categories.Update(updateObject);
                await _context.SaveChangesAsync();

                return categoryEntry.Entity;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "An error occurred while updating category from database.");
                return new Category() { Name = string.Empty };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while  updating category from database.");
                return new Category() { Name = string.Empty };
            }
        }
    }
}
