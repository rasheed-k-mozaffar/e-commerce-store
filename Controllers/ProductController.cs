using Microsoft.AspNetCore.Mvc;
using e_commerce_store.data;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;
using e_commerce_store.Models.Interfaces;

namespace e_commerce_store.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        IProductRepository _context;
        public ProductController(IProductRepository context){
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = _context.GetAll();
            return View(products);       
        }

    }
}