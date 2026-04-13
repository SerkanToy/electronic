namespace electronic.Domain.Entities.Employees
{
    public class Tags
    {
        public Tags()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public ICollection<ProductsJoinTags> ProductsJoinTags { get; set; }
    }
}
