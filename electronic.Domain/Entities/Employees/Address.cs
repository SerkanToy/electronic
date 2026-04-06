using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees;


public sealed record Address
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Town { get; set; }
    public string? FulLAddress { get; set; }
}
