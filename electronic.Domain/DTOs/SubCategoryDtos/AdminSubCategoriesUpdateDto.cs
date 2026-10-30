using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.SubCategoryDtos
{
    public class AdminSubCategoriesUpdateDto
    {
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Name { get; set; }
        public string CategoriesId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public bool IsActive { get; set; }
    }
}
