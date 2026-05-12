using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Dtos.UserDtos
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Boş Bırakmayın")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayın")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayın")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayın")]
        public string LastName { get; set; }
    }
}
