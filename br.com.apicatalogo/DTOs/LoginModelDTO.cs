using System;
using System.ComponentModel.DataAnnotations;

namespace br.com.apicatalogo.DTOs
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "Usuário requerido.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Senha requerida.")]
        public string? Password { get; set; }
    }
}

