namespace electronic.Domain.DTOs.CategoryDTOs
{
    public class AdminCategoryAllDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string icon { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public string CreateUserName { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
        public string? UpdateUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
        public string? DeleteUser { get; set; }
    }
}
