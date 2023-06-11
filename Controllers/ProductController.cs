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
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_context.GetAll == null)
            {
                return NotFound();
            }

            var product = await _context.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProductExist(product.ProductId))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.GetAll == null)
            {
                return NotFound();
            }

            var product = await _context.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GetAll == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var product = await _context.GetByIdAsync(id);
            if (product != null)
            {
                _context.Delete(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}