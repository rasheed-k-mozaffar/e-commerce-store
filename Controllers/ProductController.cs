using Microsoft.AspNetCore.Mvc;
using e_commerce_store.data;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context){
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
        var products = _context.Products
        .Include(c => c.Category)
        .AsNoTracking();
            return View(await products.ToListAsync());
        }

    }
}