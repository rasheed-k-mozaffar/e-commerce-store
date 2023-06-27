using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
using e_commerce_store.ViewModels;

namespace e_commerce_store.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public HomeController(ICategoryRepository categoryRepository,IProductRepository productRepository,ILogger<HomeController> logger)
    {
        _logger = logger;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

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

        public async Task<IActionResult> Detail(int id){
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
