using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Dtos.AddressDto
{
    public class AddressByIdDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FullAddress { get; set; }
        public string MailCode { get; set; }
    }
}
