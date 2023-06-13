using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Models
{
    public class OrderItem
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }    
        
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }    

        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }   
    }
}