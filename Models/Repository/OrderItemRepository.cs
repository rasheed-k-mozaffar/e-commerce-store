using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context){
            _context = context;
        }        
        public bool Add(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            return Save();
        }

        public bool Remove(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            return Save();
        }

        public async Task<OrderItem> GetOrderItemById(int orderItemId)
        {
            return await _context.OrderItems.AsNoTracking().SingleOrDefaultAsync(i => i.Id == orderItemId);
        }

        public bool Update(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public async Task<List<OrderItem>> GetOrderItemsList(int orderId)
        {
            return await _context.OrderItems.Where(i => i.OrderId == orderId).ToListAsync();
        }

        public async Task<bool> isProductExistsOnOrderAsync(int productId, int orderID)
        {
            return await _context.OrderItems
                .AnyAsync(ci => ci.OrderId == orderID && ci.ProductId == productId);
        }


    }
}