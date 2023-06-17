using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;

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