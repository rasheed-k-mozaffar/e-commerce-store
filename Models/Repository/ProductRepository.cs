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
            return await _context.Products.Include(item => item.Category).Include(i => i.DescriptionImages).SingleOrDefaultAsync(i => i.Id == id);
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


        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<IEnumerable<Product>> GetSliceAsync(int offset, int size)
        {
            return await _context.Products.Include(i => i.Category).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAndSliceAsync(int categoryID, int offset, int size)
        {
            return await _context.Products
                .Include(i => i.Category)
                .Where(c => c.CategoryId == categoryID)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAndSliceAsync(string searchString , int offset, int size)
        {
            return await _context.Products.AsNoTracking().Where(s => s.Name.ToLower().Contains(searchString.ToLower())).Skip(offset).Take(size).ToListAsync();
        }
        public async Task<IEnumerable<Product>> SearchByCategoryAndSliceAsync(string searchString , int categoryId , int offset, int size)
        {
            return await _context.Products.AsNoTracking().Where(s => s.Name.ToLower().Contains(searchString.ToLower()) && s.CategoryId == categoryId).Skip(offset).Take(size).ToListAsync();
        }
        public async Task<int> GetCountBySearchAsync(string searchString)
        {
            return await _context.Products.AsNoTracking().Where(s => s.Name.ToLower().Contains(searchString.ToLower())).CountAsync();
        }

        public async Task<int> GetCountBySearchWithCategoryAsync(string searchString , int categoryId)
        {
            return await _context.Products.AsNoTracking().Where(s => s.Name.ToLower().Contains(searchString.ToLower()) && s.CategoryId == categoryId).CountAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceAndSliceAsync(int priceMax,int priceMin ,int categoryID, int offset, int size)
        {
            if(categoryID == -1)
                return await _context.Products
                .Include(i => i.Category)
                .Where(c => c.Price <= priceMax && c.Price >= priceMin)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
            else
                return await _context.Products
                    .Include(i => i.Category)
                    .Where(c => c.CategoryId == categoryID &&  (c.Price <= priceMax && c.Price >= priceMin))
                    .Skip(offset)
                    .Take(size)
                    .ToListAsync();
        }
        
        public async Task<int> GetCountByCategoryAsync(int categoryID)
        {
            return await _context.Products.CountAsync(c => c.CategoryId == categoryID);
        }

        public async Task<int> GetCountByPriceAsync(int categoryID, int priceMax,int priceMin)
        {
            if(categoryID == -1)
                return await _context.Products.CountAsync(c => c.Price <= priceMax && c.Price >= priceMin);
            else
                return await _context.Products.CountAsync(c => c.CategoryId == categoryID &&  (c.Price <= priceMax && c.Price >= priceMin));
        }


    }
}