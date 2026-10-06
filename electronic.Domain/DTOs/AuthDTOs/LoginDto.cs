using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.AuthDTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Boş Bırakmayın")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayın")]
        [StringLength(100, ErrorMessage = "Şifre en az 6 karakter ve en çok 100 karakter olmalıdır.", MinimumLength = 6)]
        public string Password { get; set; } 
    }
}
