using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_store.Controllers 
{
    public class ImageController : Controller
    {
        private readonly IDescriptionImagesRepository _descriptionImagesRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public ImageController(IDescriptionImagesRepository descriptionImagesRepository ,  Microsoft.AspNetCore.Hosting.IHostingEnvironment env){
            _descriptionImagesRepository = descriptionImagesRepository;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task Delete(int productId , string imageURL){
            var img = await _descriptionImagesRepository.Get(productId,imageURL);
            var oldImagePath = Path.Combine(_env.WebRootPath, imageURL.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _descriptionImagesRepository.Remove(img);
        }

    }
}