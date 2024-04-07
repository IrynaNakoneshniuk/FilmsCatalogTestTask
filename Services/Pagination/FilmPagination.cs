using FilmsCatalogTestTask.Data.Models;

namespace FilmsCatalogTestTask.Services.Pagination
{
    public class FilmPagination
    {

        public string OrderBy { get; set; } = "desc";
        public string FilterByDate { get; set; }
        public string FilterByCategory { get; set; }
        public int TotalPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }   
        public IEnumerable<Film> Films { get; set; }

        public async Task<FilmPagination>(IQueryable films, string orderBy, string filteringByDate)
        {

        }

    }
}
