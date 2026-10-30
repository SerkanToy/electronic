using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.DTOs.SubCategoryDtos
{
    public class AdminSubCategoriesDeleteDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
