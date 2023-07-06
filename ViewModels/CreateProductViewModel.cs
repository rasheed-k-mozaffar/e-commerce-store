

using System.ComponentModel.DataAnnotations;
using e_commerce_store.Models;

namespace e_commerce_store.ViewModels
{
    public class CreateProductViewModel 
    {
    [Required]
    public string Name { get; set; }    
    [Required]
    public string SKU { get; set; } 
    [Required]
    public string Description { get; set; }
    [Required]
    public IFormFile File { get; set; }
    public List<IFormFile>? Files {get; set;}
    [Required]
    public decimal Price { get; set; }  
    [Required]
    public DateTime ReleaseDate { get; set; }   
    
    
    public List<Category>? Categories { get; set; }
    [Required(ErrorMessage = "Please select a category")]
    [RegularExpression("^(?!-1$).*", ErrorMessage = "Invalid category selection")]
    public int CategoryId { get; set; }
    
    }
}