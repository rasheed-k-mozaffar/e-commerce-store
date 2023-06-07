using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Models
{
    public class OrderItems
    {
        [Key]
        [Display(Name = "ID")]
        public int OrderItemId { get; set; }    
        
        [Display(Name = "Order ID")]
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        [Display(Name = "Product ID")]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }   
    }
}