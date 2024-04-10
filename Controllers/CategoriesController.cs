using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FilmsCatalogTestTask.Data.Models;
using FilmsCatalogTestTask.Data.Repositories;

namespace FilmsCatalogTestTask.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repositoryCategory;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryRepository repositoryCategory, ILogger<CategoriesController> logger)
        {
            _repositoryCategory = repositoryCategory;
            _logger = logger;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var filmsCatalogContext = await _repositoryCategory.GetAllAsync();
            return View(filmsCatalogContext);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _repositoryCategory.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _repositoryCategory.GetAllAsync();
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentCategoryId")] Category category)
        {
            if (ModelState.IsValid)
            {
              await _repositoryCategory.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _repositoryCategory.GetAllAsync();
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Name", category.ParentCategoryId);

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await  _repositoryCategory.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categories = await _repositoryCategory.GetAllAsync();
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Name", category.ParentCategoryId);

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ParentCategoryId")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _repositoryCategory.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _repositoryCategory.GetAllAsync();
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Name", category.ParentCategoryId);

            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _repositoryCategory.GetByIdAsync(id);
                
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var category = await _repositoryCategory.GetByIdAsync(id);
            if (category != null)
            {
  
               await _repositoryCategory.DeleteAsync(category);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
