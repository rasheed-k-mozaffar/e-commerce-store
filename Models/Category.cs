using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.Models
{

    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string Name { get; set; }
    }
}