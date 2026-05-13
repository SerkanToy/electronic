using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Dtos.CountryDto
{
    public class CountryAddDto
    {
        [Required(ErrorMessage = "Ülke adı zorunludur.")]
        public string Name { get; set; }
    }
}
