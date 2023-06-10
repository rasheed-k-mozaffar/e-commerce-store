using Microsoft.AspNetCore.Mvc;
using e_commerce_store.data;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;
using e_commerce_store.Models.Interfaces;

namespace e_commerce_store.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _context;
        public ProductController(IProductRepository context){
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.GetAll();
            return View(products);       
        }

        public async Task<IActionResult> Detail(int id){
            var product = await _context.GetByIdAsync(id);
            // if (product == null)
            // {
            //     return NotFound();
            // }
            return View(product);
        }

    }
}