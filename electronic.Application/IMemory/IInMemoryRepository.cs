using electronic.Domain.Entities.Employees.Basket;

namespace electronic.Application.IMemory
{
    public interface IInMemoryRepository
    {
        Task<Basket?> GetBasketAsync(string basketId);
        Task<Basket?> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
