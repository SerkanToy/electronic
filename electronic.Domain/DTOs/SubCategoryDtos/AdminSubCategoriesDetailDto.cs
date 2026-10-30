namespace electronic.Domain.DTOs.SubCategoryDtos
{
    public class AdminSubCategoriesDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string icon { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreateUserName { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
        public string? UpdateUser { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
        public string? DeleteUser { get; set; }
    }
}
