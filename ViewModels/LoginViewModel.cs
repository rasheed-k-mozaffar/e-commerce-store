using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Email Address/User Name")]
        [Required(ErrorMessage = "Email address/User Name is required")]
        public string Identifier { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}