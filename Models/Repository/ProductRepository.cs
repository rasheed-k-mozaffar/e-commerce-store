using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context ){
            _context= context;
        }

        public bool Add(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();
        }
        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(item => item.Category).AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int? id)
        {
            return await _context.Products.Include(item => item.Category).SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int category)
        {
            return await _context.Products.Include(item => item.Category).AsNoTracking().Where(i => i.CategoryId == category).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool ProductExist(int id){
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}