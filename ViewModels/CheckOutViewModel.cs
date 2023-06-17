using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace e_commerce_store.ViewModels
{
    public class CheckOutViewModel
    {
        public string? UserName { get; set; }  
        public string? UserEmail { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string FirstName { get; set; }   
        [Required]
        public string LastName { get; set; }    
        [Required]
        public string City { get; set; }    
        [Required]
        public string Address1 { get; set; }    
        [Required]
        public string Address2 { get; set; }  
        public decimal TotalPrice { get; set; }
    }
}