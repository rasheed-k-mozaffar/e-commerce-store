using Microsoft.AspNetCore.Mvc;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;
using e_commerce_store.Models.Interfaces;
using e_commerce_store.ViewModels;
using Microsoft.AspNetCore.Authorization;
using e_commerce_store.data;

namespace e_commerce_store.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public ProductController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env,IProductRepository productRepository ,ICategoryRepository categoryRepository){
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _env = env;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll();
            return View(products);       
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id){
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create(){
            var productVM = new CreateProductViewModel();
            productVM.Categories = _categoryRepository.GetAll();
            return View(productVM);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
            var fileName = Path.GetRandomFileName();
            // Get the file extension
            var extension = Path.GetExtension(productVM.File.FileName);
            // Combine the file name and extension
            fileName = Path.ChangeExtension(fileName, extension);
            var filePath = Path.Combine(_env.WebRootPath, "Image/"+fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await productVM.File.CopyToAsync(fileStream);
            }

                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    ImageURL = "/Image/"+fileName,
                    SKU = productVM.SKU,
                    Price = productVM.Price,
                    ReleaseDate = productVM.ReleaseDate,
                    CategoryId = productVM.CategoryId
                };
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return Create();//for recreat the categories list and send it again
        }


        // GET: Movies/Edit/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(int id)
        {
            if (_productRepository.GetAll == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(int id,Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productRepository.ProductExist(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Movies/Delete/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (_productRepository.GetAll == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_productRepository.GetAll == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                _productRepository.Delete(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}