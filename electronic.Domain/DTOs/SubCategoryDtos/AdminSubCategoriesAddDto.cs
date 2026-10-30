using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.SubCategoryDtos
{
    public class AdminSubCategoriesAddDto
    {
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Name { get; set; }
        public string? CategoriesId { get; set; }
    }
}
