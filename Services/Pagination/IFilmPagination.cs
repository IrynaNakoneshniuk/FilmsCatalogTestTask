using FilmsCatalogTestTask.Data.Models;

namespace FilmsCatalogTestTask.Services.Pagination
{
    public interface IFilmPagination
    {
        IEnumerable<Film> Films { get; set; }
        string? FilterByCategory { get; set; }
        string? FilterByDate { get; set; }
        string? OrderBy { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalPage { get; set; }

        IQueryable<Film> ApplyFilters(string? orderBy = null, string? filterByDate = null, string? filterByCategory = null);
        Task<FilmPagination> GetFilmsPage(int? pageNumber,string? orderBy = null, string? filterByDate = null, string? filterByCategory = null);
    }
}