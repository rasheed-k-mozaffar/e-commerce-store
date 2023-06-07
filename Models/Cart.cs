using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_store.Models
{
    public class Cart
    {
        [Key]
        [Display(Name = "ID")]
        public int CartId { get; set; }

        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "Cart Items")]
        public List<CartItems>? CartItems { get; set; }

        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(18, 2)")]
        [RegularExpression(@"^[1-9]\d{0,7}(?:\.\d{1,4})?|\.\d{1,4}$")]
        public decimal TotalPrice { get; set; } 






    }
}