using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.DTOs.CategoryDTOs
{
    public class AdminCategoryDeleteDto
    {
        public string Name { get; set; }
        public string icon { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
