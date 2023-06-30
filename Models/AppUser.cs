using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_store.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }   }
}

