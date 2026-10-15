using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.CategoryDTOs
{
    public class AdminCategoryAddDto
    {
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Description { get; set; }
        public string? icon { get; set; }
    }
}
