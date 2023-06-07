using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace e_commerce_store.Models
{
    public class CartItems
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int CartItemId { get; set; } 

        [Display(Name = "Cart ID")]
        [ForeignKey("CartId")]
        [Required]
        public int CartId { get; set; }
 
        [Display(Name = "Product ID")]
        [ForeignKey("ProductId")]
        [Required]
        public int ProductId { get; set; } 

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime CreatedAt { get; set; }

        
        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedAt { get; set; }  
        
    }
}