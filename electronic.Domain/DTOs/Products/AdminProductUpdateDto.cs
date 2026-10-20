using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.Products
{
    public class AdminProductUpdateDto
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
