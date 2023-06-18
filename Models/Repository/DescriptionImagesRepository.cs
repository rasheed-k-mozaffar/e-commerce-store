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
        private readonly IWebHostEnvironment _env;
        public DescriptionImagesRepository(IWebHostEnvironment env,ApplicationDbContext context){
            _context = context;
            _env = env;
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
                var imagePath = Path.Combine(_env.WebRootPath, img.URL.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
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