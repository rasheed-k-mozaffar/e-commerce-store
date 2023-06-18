

using e_commerce_store.Models;

namespace e_commerce_store.ViewModels
{
    public class EditProductViewModel 
    {
        public int? Id { get; set; }
        public string Name { get; set; }    
        public string SKU { get; set; } 
        public string Description { get; set; }
        public IFormFile? File { get; set; }
        public List<IFormFile>? Files {get; set;}
        public decimal Price { get; set; }  
        public DateTime ReleaseDate { get; set; }   
    
        public List<Category>? Categories { get; set; }
        public int CategoryId { get; set; }

        public string? CurrentImageURL { get; set; }
        public List<string>? CurrentDescriptionImageURLs { get; set; }
    }
}