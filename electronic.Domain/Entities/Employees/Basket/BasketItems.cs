namespace electronic.Domain.Entities.Employees.Basket
{
    public class BasketItems
    {
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public decimal Discount { get; set; } = decimal.Zero;
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
