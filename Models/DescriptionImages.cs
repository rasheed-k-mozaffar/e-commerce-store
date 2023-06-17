using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Models
{
    public class DescriptionImages
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }  
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public string URL { get; set; }
    }
}