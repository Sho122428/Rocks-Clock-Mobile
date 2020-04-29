using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RockClockMobile.Models
{
    public class ChangePasswordVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Must be between 4 and 255 characters", MinimumLength = 4)]
        public string Password { get; set; }
        [Required, Compare("Password")]
        [StringLength(255, ErrorMessage = "Must be between 4 and 255 characters", MinimumLength = 4)]
        public string ConfirmPassword { get; set; }
        public string currentPassword { get; set; }
        public bool isWeb { get; set; }
    }
}
