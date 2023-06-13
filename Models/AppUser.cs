

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_store.Models
{
    public class AppUser
    {

        [Key]
        public int Id { get; set; }  

        public string FirstName { get; set; }   

        public string  LastName { get; set; }   

        public string Email { get; set; }   

    }
}