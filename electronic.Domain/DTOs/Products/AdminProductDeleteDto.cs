namespace electronic.Domain.DTOs.Products
{
    public class AdminProductDeleteDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
