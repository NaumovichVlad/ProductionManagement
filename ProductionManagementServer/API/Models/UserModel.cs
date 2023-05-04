﻿using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}