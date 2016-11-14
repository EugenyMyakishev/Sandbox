using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AspCoreFirst.Models.EntityHelpers
{
    public class UserLoginHelper
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(1), MaxLength(10)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
