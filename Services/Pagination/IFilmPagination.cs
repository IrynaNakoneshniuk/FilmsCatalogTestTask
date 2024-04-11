using FilmsCatalogTestTask.Data.Models;

namespace FilmsCatalogTestTask.Services.Pagination
{
    public interface IFilmPagination
    {
        IEnumerable<Film> Films { get; set; }
        int PageNumber { get; set; }
        int  PageSize { get; set; }
        int TotalPage { get; set; }
        IQueryable<Film> ApplyFilters(string? orderBy = null, string? filterByDate = null, int? filterByCategory = null, string? filterByDirector = null);
        Task<FilmPagination> GetFilmsPage(int pageNumber, string? orderBy = null, string? filterByDate = null, int? filterByCategory = null, string? filterByDirector = null);
    }
}