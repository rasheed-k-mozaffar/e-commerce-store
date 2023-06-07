using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace e_commerce_store.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }    

        [ForeignKey("UserId")]
        public int UserId { get; set; }


        [MinLength(1)]
        [Required]
        public List<OrderItems>? OrderItems { get; set; }  

        [Display(Name = "First Name")]
        [Required]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Address { get; set; }

        [Display(Name = "Address 2")]
        [Required]
        public string? Address2 { get; set; }

        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? CreditCard { get; set; }


    }
}