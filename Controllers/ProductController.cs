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

        private IWebHostEnvironment _env;


        public ProductController(IWebHostEnvironment env,IProductRepository productRepository ,ICategoryRepository categoryRepository,IDescriptionImagesRepository descriptionImagesRepository){
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _env = env;
            _descriptionImagesRepository =descriptionImagesRepository;
        }

        [AllowAnonymous]
        [Route("Products")]
        public async Task<IActionResult> Index(string? searchString, int categoryId = -1, int page = 1, int pageSize = 3)
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
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = _categoryRepository.GetAll();

            var viewModel = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                Price = product.Price,
                ReleaseDate = product.ReleaseDate,
                CategoryId = product.CategoryId,
                Categories = categories,
                CurrentImageURL = product.ImageURL,
                CurrentDescriptionImageURLs = product.DescriptionImages.Select(di => di.URL).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(EditProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var product = await _productRepository.GetByIdAsync(productVM.Id);

                if (product == null)
                {
                    return NotFound();
                }

                // Delete old image if a new one is uploaded
                if (productVM.File != null)
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, product.ImageURL.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // Upload new image
                    var fileName = Path.GetRandomFileName();
                    var extension = Path.GetExtension(productVM.File.FileName);
                    fileName = Path.ChangeExtension(fileName, extension);
                    var filePath = Path.Combine(_env.WebRootPath, "Image", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productVM.File.CopyToAsync(fileStream);
                    }

                    product.ImageURL = "/Image/" + fileName;
                }

                product.Name = productVM.Name;
                product.Description = productVM.Description;
                product.SKU = productVM.SKU;
                product.Price = productVM.Price;
                product.ReleaseDate = productVM.ReleaseDate;
                product.CategoryId = productVM.CategoryId;

                if(productVM.Files != null)
                await UploadDescriptionImages(productVM.Files,product.Id);

                _productRepository.Update(product);

                return RedirectToAction("Products", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(productVM);
        }


        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task Delete(int? productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, product.ImageURL.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _descriptionImagesRepository.ClearProductImages(product.Id);
                _productRepository.Delete(product);
            }
        }


    }
}