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
        [Required(ErrorMessage = "Boş Bırakmayın")]
        //[RegularExpression(@"^(\+90|0090|90|0)?[\s\-]?5[0-9]{2}[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$",ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [RegularExpression(@"^(\+90|0)?[\s\-]?5[0-9]{2}[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
    }
}
