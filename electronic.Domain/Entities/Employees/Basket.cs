namespace electronic.Domain.Entities.Employees
{
    public class Basket 
    {
        public string Id { get; set; } = string.Empty;
        public List<BasketItems> BasketItems { get; set; } = new List<BasketItems>();
    }
}
