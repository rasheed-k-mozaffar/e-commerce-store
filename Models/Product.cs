using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_store.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int ProductId { get; set; }
        public string? SKU { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [RegularExpression(@"^[1-9]\d{0,7}(?:\.\d{1,4})?|\.\d{1,4}$")]
        [Required]   
        public decimal Price { get; set; }  

        public string? Derscription { get; set; }

        public string? ImageURL { get; set; }    

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Category ID")]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; } 

    }
}