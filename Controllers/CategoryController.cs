using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_store.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryController(ICategoryRepository categoryRepository , IProductRepository productRepository){
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return RedirectToAction("Categories", "Dashboard"); // Redirect to the home page (change the appropriate action and controller names)
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int id) {
            var category = await _categoryRepository.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category) {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction("Orders", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(category);
        }

        
        [HttpPost]
        public async Task Delete(int id){
            var category = await _categoryRepository.GetByIdAsync(id);
            await _productRepository.DeleteAllProductByCategoryID(category.Id);
            _categoryRepository.Delete(category);
        }

    }
}