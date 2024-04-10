using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogTestTask.Services.Pagination
{
    public class FilmPagination : IFilmPagination
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ILogger<FilmPagination> _logger;
        public string? OrderBy { get; set; }
        public string? FilterByDate { get; set; }
        public string? FilterByCategory { get; set; }
        public int TotalPage { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public IEnumerable<Film> Films { get; set; } = new List<Film>();

        public FilmPagination(IFilmRepository filmRepository, ILogger<FilmPagination> logger)
        {
            _filmRepository = filmRepository;
            _logger = logger;
        }

        public FilmPagination()
        {
        }

        public IQueryable<Film> ApplyFilters(string? orderBy = default, string? filterByDate = default,
            string? filterByCategory = default)
        {
            try
            {
                var filteredFilmsQuery = _filmRepository.GetAll();

                if (orderBy == "desc")
                {
                    filteredFilmsQuery = filteredFilmsQuery.OrderByDescending(f => f.Release);
                }
                else if (orderBy == "asc")
                {
                    filteredFilmsQuery = filteredFilmsQuery.OrderBy(f => f.Release);
                }

                if (!string.IsNullOrEmpty(filterByDate))
                {
                    DateTime releaseDate = DateTime.Parse(filterByDate);
                    filteredFilmsQuery = filteredFilmsQuery.Where(f => f.Release == releaseDate);
                }

                if (!string.IsNullOrEmpty(filterByCategory))
                {
                    filteredFilmsQuery = filteredFilmsQuery.Where(f => f.FilmCategories.Any(fc => fc.Category.Name.Equals(filterByCategory)));
                }

                return filteredFilmsQuery;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while  apply filters.");
                return new List<Film>().AsQueryable();
            }
        }

        public async Task<FilmPagination> GetFilmsPage(int? pageNumber,string? orderBy = default, string? filterByDate = default,
            string? filterByCategory = default)
        {
            try
            {
                var films = ApplyFilters(orderBy, filterByDate, filterByCategory); 
                int amountFilms = films.Count();
                
                TotalPage = (int)Math.Ceiling((double)amountFilms / PageSize);
                PageNumber = (int)pageNumber;

                Films = await films.Skip(PageSize * (PageNumber - 1))
                     .Take(PageSize)
                     .AsNoTracking()
                     .ToListAsync();
                     
                return new FilmPagination()
                {
                    TotalPage = this.TotalPage,
                    Films = this.Films,
                    PageNumber = this.PageNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting films page.");
                return new FilmPagination();
            }
        }
    }
}
