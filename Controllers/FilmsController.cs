using Microsoft.AspNetCore.Mvc;
using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Services.Pagination;
using FilmsCatalogTestTask.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmsCatalogTestTask.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IFilmPagination _filmPagination;
        private readonly IFilmRepository _filmRepository;
        private readonly ICategoryRepository _repositoryCategory;

        public FilmsController(IFilmPagination filmPagination, IFilmRepository filmRepository,
             ICategoryRepository repositoryCategory)
        {
            _filmPagination = filmPagination;
            _filmRepository = filmRepository;
            _repositoryCategory=repositoryCategory;
        }

        // GET: Films
        public async Task<IActionResult> Index(int pageNumber, int ?category,string sortByDate, DateTime? releaseDate, string director)
        {
            var categories = await _repositoryCategory.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            var films = await _filmPagination.GetFilmsPage(pageNumber,  sortByDate, releaseDate.ToString(), category, director);

            if (films == null)
            {
                return BadRequest();
            }
            return View(films);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var film = await _filmRepository.GetByIdAsync(id);

            if (film == null)
            {
                return BadRequest();
            }
            return View(film);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Film? film)
        {
            if (film == null)
            {
                return BadRequest();
            }
            await _filmRepository.UpdateAsync(film);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Film? film)
        {
            if (film == null)
            {
                return BadRequest();
            }
            await _filmRepository.DeleteAsync(film);

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var film = await _filmRepository.GetByIdAsync(id);

            if (film == null)
            {
                return BadRequest();
            }
            return View(film);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var film = await _filmRepository.GetByIdAsync(id);

            if (film == null)
            {
                return BadRequest();
            }
            return View(film);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Film? film, List<int> categories)
        {
            if (film == null)
            {
                return BadRequest();
            }
            await _filmRepository.CreateAsync(film,categories); 
            return RedirectToAction(nameof(Index));  
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _repositoryCategory.GetAllAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");

            return View();
        }
    }
}
