using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogTestTask.Services.Pagination
{
    public class FilmPagination : IFilmPagination
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ILogger<FilmPagination> _logger;
        public int TotalPage { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
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
            int? filterByCategory = default, string? filterByDirector = default)
        {
            try
            {
                var filteredFilmsQuery = _filmRepository.GetAllQueryable();

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

                if (filterByCategory != null)
                {
                    filteredFilmsQuery = filteredFilmsQuery.Where(f => f.FilmCategories.Any(fc => fc.Category.Id.Equals(filterByCategory)));
                }

                if (!string.IsNullOrEmpty(filterByDirector))
                {
                    filteredFilmsQuery = filteredFilmsQuery.Where(f => f.Director.Equals(filterByDirector));
                }

                return filteredFilmsQuery;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while  apply filters.");
                return new List<Film>().AsQueryable();
            }
        }

        public async Task<FilmPagination> GetFilmsPage(int pageNumber, int? sizePage , string? orderBy = default, string? filterByDate = default,
            int? filterByCategory = default, string? filterByDirector = default)
        {
            try
            {
                var films = ApplyFilters(orderBy, filterByDate, filterByCategory, filterByDirector);
                int amountFilms = films.Count();

                PageNumber = pageNumber;
                PageSize = sizePage??10;
                TotalPage = (int)Math.Ceiling((double)(amountFilms / PageSize));
               
                Films = await films.Skip(PageSize * (PageNumber - 1))
                     .Take(PageSize)
                     .AsNoTracking()
                     .ToListAsync();

                return new FilmPagination()
                {
                    TotalPage = this.TotalPage,
                    Films = this.Films,
                    PageNumber = this.PageNumber,
                    PageSize=this.PageSize
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
