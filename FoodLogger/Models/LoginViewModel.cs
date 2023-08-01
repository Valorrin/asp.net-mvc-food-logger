﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace FoodLogger.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Emal Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
