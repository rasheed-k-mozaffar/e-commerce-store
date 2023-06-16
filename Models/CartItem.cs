using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace e_commerce_store.Models
{
    public class CartItem
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; } 

        [Display(Name = "Cart ID")]
        [Required]
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }  
 
        [Display(Name = "Product ID")]
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }    

        [Required]
        public int Quantity { get; set; }           
    }
}