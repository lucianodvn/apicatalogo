using System;
using Microsoft.AspNetCore.Identity;

namespace br.com.apicatalogo.Models.Tokens
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

