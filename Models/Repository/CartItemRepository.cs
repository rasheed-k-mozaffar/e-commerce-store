using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context){
            _context = context;
        }        
        public bool Add(CartItem CartItem)
        {
            _context.CartItems.Add(CartItem);
            return Save();
        }

        public bool Remove(CartItem CartItem)
        {
            _context.CartItems.Remove(CartItem);
            return Save();
        }

        public async Task<CartItem> GetCartItemById(int CartItemId)
        {
            return await _context.CartItems.AsNoTracking().SingleOrDefaultAsync(i => i.Id == CartItemId);
        }

        public bool Update(CartItem CartItem)
        {
            _context.CartItems.Update(CartItem);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<List<CartItem>> GetCartItemsList(int CartId)
        {
            return await _context.CartItems.Where(i => i.CartId == CartId).ToListAsync();
        }

        public async Task<bool> isProductExistsOnCartAsync(int ProductId, int CartID)
        {
            return await _context.CartItems
                .AnyAsync(ci => ci.CartId == CartID && ci.ProductId == ProductId);
        }
    }
}