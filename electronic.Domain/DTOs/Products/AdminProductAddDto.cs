using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.Products
{
    public class AdminProductAddDto
    {
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Boş Bırakmayınız")]
        public string SubCategoryId { get; set; }
    }
}
