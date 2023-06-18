using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class DescriptionImagesRepository : IDescriptionImagesRepository
    {
        private readonly ApplicationDbContext _context;
        public DescriptionImagesRepository(ApplicationDbContext context){
            _context = context;
        }
        public bool Add(DescriptionImages image)
        {
            _context.Add(image);
            return Save();
        }

        public async Task<DescriptionImages> Get(int productId,string url)
        {
            return await _context.DescriptionImages.Where(i => i.ProductId == productId).SingleOrDefaultAsync(i => i.URL.Equals(url));
        }

        public bool ClearProductImages(int productId)
        {
            var images = _context.DescriptionImages.Where(i => i.ProductId == productId).ToList();
            foreach(var img in images){
                if (System.IO.File.Exists(img.URL))
                {
                    System.IO.File.Delete(img.URL);
                }
                _context.Remove(img);
            }
            return Save();
        }

        public bool Remove(DescriptionImages image)
        {
            _context.Remove(image);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}