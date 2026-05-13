using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Dtos.AddressDto
{
    public class AddressAddDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string MailCode { get; set; }
        [Required]
        public Guid UserAppId { get; set; }
        [Required]
        public Guid CountryId { get; set; }
        [Required]
        public Guid TownId { get; set; }
        [Required]
        public Guid CityId { get; set; }
    }
}
