using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlteraBarberShop.Models
{
    public class NewUser
    {
        [Required(ErrorMessage ="Please Enter the UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter the Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter the FirtName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter the LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter the ContactNumber")]
        public long ContactNumber { get; set; }
        [Required(ErrorMessage = "Please Enter the Address")]
        public string Address { get; set; }
        
    }
}