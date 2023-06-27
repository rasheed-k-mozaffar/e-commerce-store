
using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(IProductRepository productRepository,ApplicationDbContext context ){
            _context= context;
        }

        public bool Add(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
        public bool Delete(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public  List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public async Task<Category?> GetByIdAsync(int? id)
        {
            return await _context.Categories.SingleOrDefaultAsync(i => i.Id == id);
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool CategoryExist(int id){
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IEnumerable<Category>> GetSliceAsync(int offset, int size)
        {
            return await _context.Categories.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Category>> SearchAndSliceAsync(string searchString, int offset, int size)
        {
            return await _context.Categories.AsNoTracking().Where(s => s.Name.ToLower().Contains(searchString.ToLower())).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.AsNoTracking().CountAsync();
        }

        public async Task<int> GetCountBySearchAsync(string searchString)
        {
            return await _context.Categories.AsNoTracking().Where(s => s.Name.ToLower().Contains(searchString.ToLower())).CountAsync();
        }
    }
}