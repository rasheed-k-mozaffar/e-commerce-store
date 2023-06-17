namespace e_commerce_store.Models.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetOrderItemById(int orderItemId);
        Task<List<OrderItem>> GetOrderItemsList(int orderId);
        Task<bool> isProductExistsOnOrderAsync(int productId,int cartID);
        bool Add(OrderItem orderItem);
        bool Update(OrderItem orderItem);
        bool Remove(OrderItem orderItem);
        bool Save();
    }
}