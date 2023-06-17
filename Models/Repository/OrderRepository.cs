using e_commerce_store.data;
using e_commerce_store.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderItemRepository _OrderItemRepository;
        public OrderRepository(ApplicationDbContext context,IOrderItemRepository OrderItemRepository){
            _context = context;
            _OrderItemRepository = OrderItemRepository;
        }

        public async Task<Order> GetOrderByUserId(string? userId)
        {
            return await _context.Orders.Include(c => c.OrderItems).ThenInclude(ci => ci.Product).AsNoTracking().SingleOrDefaultAsync(i => i.AppUserId.Equals(userId));
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.Include(c => c.OrderItems).ThenInclude(ci => ci.Product).AsNoTracking().SingleOrDefaultAsync(i => i.Id == id);
        }

        public Order MakeOrderForUser(string userId)
        {
            var Order = new Order {
                AppUserId = userId
            };
            _context.Orders.Add(Order);
            _context.SaveChanges();
            return Order;
        }

        public async Task ClearOrderItems(int OrderId)
        {
            var Order = await GetOrderById(OrderId);
            var itemsToRemove = Order.OrderItems.ToList();
            foreach(var item in itemsToRemove){
                _OrderItemRepository.Remove(item);
            }
            Save();
        }

        public bool Add(Order order){
            _context.Orders.Add(order);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}