namespace e_commerce_store.Models.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetCartItemById(int CartItemId);
        Task<List<CartItem>> GetCartItemsList(int CartId);
        Task<bool> isProductExistsOnCartAsync(int ProductId,int CartID);
        bool Add(CartItem CartItem);
        bool Update(CartItem CartItem);
        bool Remove(CartItem CartItem);
        bool Save();
    }
}