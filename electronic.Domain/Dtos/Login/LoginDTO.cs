using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Dtos.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Boş Bırakmayın.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayın.")]
        public string Password { get; set; }
    }
}
