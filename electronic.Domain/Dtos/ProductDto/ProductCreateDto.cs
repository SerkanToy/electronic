using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Dtos.ProductDto
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Boş birakmayınız.")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? icon { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required]
        [Range(0, 999999999999999, ErrorMessage = "0 ile *** arasında bir değer giriniz.")]
        public decimal RegulerPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required]
        [Range(0, 999999999999999, ErrorMessage = "0 ile *** arasında bir değer giriniz.")]
        public decimal DiscountPrice { get; set; }
        public string? Note { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
