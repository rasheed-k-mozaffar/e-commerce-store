using System.Linq;
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
        IDescriptionImagesRepository _descriptionImagesRepository;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public ProductController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env,IProductRepository productRepository ,ICategoryRepository categoryRepository,IDescriptionImagesRepository descriptionImagesRepository){
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _env = env;
            _descriptionImagesRepository =descriptionImagesRepository;
        }

        [AllowAnonymous]
        [Route("Products")]
        public async Task<IActionResult> Index(string searchString, int categoryId = -1, int page = 1, int pageSize = 3)
        {
            if (page < 1 || pageSize < 1 || categoryId < -1 || pageSize > 40)
            {
                return NotFound();
            }

            IEnumerable<Product> products;
            int count;

            if (!String.IsNullOrEmpty(searchString) && !searchString.Equals(""))
            {
                if(categoryId == -1){
                    products = await _productRepository.SearchAndSliceAsync(searchString,(page - 1) * pageSize, pageSize);
                    count = await _productRepository.GetCountBySearchAsync(searchString);
                }else{
                    products = await _productRepository.SearchByCategoryAndSliceAsync(searchString,categoryId,(page - 1) * pageSize, pageSize);
                    count = await _productRepository.GetCountBySearchWithCategoryAsync(searchString,categoryId);
                }
            }else
            {
                count = categoryId switch
                {
                    -1 => await _productRepository.GetCountAsync(),
                    _ => await _productRepository.GetCountByCategoryAsync(categoryId),
                };
                products = categoryId switch
                {
                    -1 => await _productRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                    _ => await _productRepository.GetProductsByCategoryAndSliceAsync(categoryId, (page - 1) * pageSize, pageSize),
                };
                
            }

            var productViewModel = new IndexProductViewModel
            {
                Products = products,
                Page = page,
                PageSize = pageSize,
                TotalProducts = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                CategoryId = categoryId,
                Categories = _categoryRepository.GetAll(),
                searchString = searchString
            };

            return View(productViewModel);    
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
                CategoryId = productVM.CategoryId,
            };
            _productRepository.Add(product);
            
            if(productVM.Files != null)
                await UploadDescriptionImages(productVM.Files,product.Id);
            
            return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return Create();//for recreat the categories list and send it again
        }

        private async Task UploadDescriptionImages(List<IFormFile> files , int productId)
        {
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetRandomFileName();
                    // Get the file extension
                    var extension = Path.GetExtension(formFile.FileName);
                    // Combine the file name and extension
                    fileName = Path.ChangeExtension(fileName, extension);
                    var filePath = Path.Combine(_env.WebRootPath, "Image/Description/"+fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    } 
                    var image = new DescriptionImages {
                        ProductId = productId,
                        URL = "/Image/Description/"+fileName
                    };
                    _descriptionImagesRepository.Add(image);
                }
            }
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