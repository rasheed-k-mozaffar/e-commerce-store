
using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context ){
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
    }
}