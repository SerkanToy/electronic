using electronic.Domain.Dtos.AddressDto;
using electronic.Domain.Entities.Employees.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Dtos.CountryDto
{
    public class CountryByIdDto
    {
        public CountryByIdDto()
        {
            Address = new List<AddressListDTO>();
            City = new List<City>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<AddressListDTO> Address { get; set; }
    }
}
