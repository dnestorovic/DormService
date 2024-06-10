﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class UserCredentialsDTO
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }
    }
}
