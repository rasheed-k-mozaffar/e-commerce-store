namespace e_commerce_store.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order MakeOrderForUser(string userId);
        Task<Order> GetOrderByUserId(string? userId);
        Task<Order> GetOrderById(int id);

        bool Add(Order order);

        Task ClearOrderItems(int cartId);
        bool Save();
    }
}