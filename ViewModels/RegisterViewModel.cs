using System.ComponentModel.DataAnnotations;

namespace e_commerce_store.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        [Display(Name ="User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}