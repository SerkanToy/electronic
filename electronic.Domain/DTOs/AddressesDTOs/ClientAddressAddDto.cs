using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.AddressesDTOs
{
    public class ClientAddressAddDto
    {
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }
        [Required(ErrorMessage = "*")]
        public string FullAddress { get; set; }
        [Required(ErrorMessage = "*")]
        public string MailCode { get; set; }
        public string CountryId { get; set; }
        [Required(ErrorMessage = "*")]
        public string CityId { get; set; }
        [Required(ErrorMessage = "*")]
        public string TownId { get; set; }
    }
}
