using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Dtos.Categoris
{
    public class AddCategoriDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; } 
    }
}
