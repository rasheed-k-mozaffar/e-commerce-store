using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartItemRepository _cartItemRepository;
        public CartRepository(ApplicationDbContext context,ICartItemRepository cartItemRepository){
            _context = context;
            _cartItemRepository = cartItemRepository;
        }
        public bool ClearCartItems()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByUserId(string? userId)
        {
            return await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).AsNoTracking().SingleOrDefaultAsync(i => i.AppUserId.Equals(userId));
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).AsNoTracking().SingleOrDefaultAsync(i => i.Id == id);
        }

        public Cart MakeCartForUser(string userId)
        {
            var cart = new Cart {
                AppUserId = userId
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        public async Task ClearCartItems(int cartId)
        {
            var cart = await GetCartById(cartId);
            var itemsToRemove = cart.CartItems.ToList();
            foreach(var item in itemsToRemove){
                _cartItemRepository.Remove(item);
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}