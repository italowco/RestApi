using System.ComponentModel.DataAnnotations;

namespace RestApi.Domain.TO
{ 
    public class RegisterTO
    {
        [Required(ErrorMessage = "Nome do usuário necessário")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email requerido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password é necessário")]
        public string? Password { get; set; }
    }
}
