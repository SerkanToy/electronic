using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Dtos.CategoriDto
{
    public class AddCategoriDTO
    {
        [Required(ErrorMessage = "Boş Bırakmayın")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayın")]
        public string Description { get; set; }
        public string? icon { get; set; } 
    }
}
