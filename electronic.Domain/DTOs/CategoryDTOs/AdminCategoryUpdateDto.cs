using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.CategoryDTOs
{
    public class AdminCategoryUpdateDto
    {
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Name { get; set; }
        public string? icon { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public bool IsActive { get; set; } 
    }
}
