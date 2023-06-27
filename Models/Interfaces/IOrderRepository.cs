namespace e_commerce_store.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order MakeOrderForUser(string userId);
        Task<Order?> GetOrderByUserId(string? userId);
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<Order>> GetSliceAsync(int offset, int size);
        Task<IEnumerable<Order>> SearchAndSliceAsync(string searchString , int offset, int size);
        Task<int> GetCountAsync();
        Task<int> GetCountBySearchAsync(string searchString );
        bool Add(Order order);
        bool Remove(Order order);
        bool Update(Order order);
        Task ClearOrderItems(int cartId);
        bool Save();
    }
}