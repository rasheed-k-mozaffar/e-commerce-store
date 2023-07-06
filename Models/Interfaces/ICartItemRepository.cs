namespace e_commerce_store.Models.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetCartItemById(int cartItemId);
        Task<List<CartItem>> GetCartItemsList(int cartId);
        Task<CartItem> isProductExistsOnCartAsync(int productId,int cartID);
        bool Add(CartItem cartItem);
        bool Update(CartItem cartItem);
        bool Remove(CartItem cartItem);
        bool Save();
    }
}