namespace e_commerce_store.Models.Interfaces
{
    public interface ICartRepository
    {
        Cart MakeCartForUser(string userId);
        Task<Cart> GetCartByUserId(string? userId);
        Task<Cart> GetCartById(int id);
        Task ClearCartItems(int cartId);

        Task<decimal> GetTotalPrice(Cart cart);
        bool Save();
    }
}