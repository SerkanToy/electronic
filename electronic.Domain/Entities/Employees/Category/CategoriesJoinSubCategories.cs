using electronic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees.Category
{
    public class CategoriesJoinSubCategories : Entity
    {
        public Categories Categories { get; set; }
        public Guid CategoriesId { get; set; }
        public SubCategories SubCategories { get; set; }
        public Guid SubCategoriesId { get; set; }
    }
}
