using FilmsCatalogTestTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogTestTask.Data.ModelsDB;

public partial class FilmsCatalogContext : DbContext
{
    public FilmsCatalogContext()
    {
    }

    public FilmsCatalogContext(DbContextOptions<FilmsCatalogContext> options)
        : base(options)
    {
    }

    public DbSet<Film> Films { get; set; }
    public DbSet<FilmCategory> FilmCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
}
