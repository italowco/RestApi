using System.ComponentModel.DataAnnotations;


namespace RestApi.Domain.TO
{
    public class LoginTO
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "O password é obrigatório.")]
        public string? Password { get; set; }
    }
}
