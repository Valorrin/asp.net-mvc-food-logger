﻿using System.ComponentModel.DataAnnotations;

namespace FoodLogger.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email adress is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
