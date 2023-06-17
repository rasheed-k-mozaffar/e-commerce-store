using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace e_commerce_store.Controllers
{
   [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderItemRepository _OrderItemRepository;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(UserManager<AppUser> userManager,IOrderRepository OrderRepository, IOrderItemRepository OrderItemRepository)
        {
            _userManager = userManager;
            _OrderRepository = OrderRepository;
            _OrderItemRepository = OrderItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }
            var Order = await _OrderRepository.GetOrderByUserId(user.Id);
            if(Order == null)
                Order = _OrderRepository.MakeOrderForUser(user.Id);

            return View(Order);
        }

        [HttpPost]
        public async Task<IActionResult> AddToOrder(int ProductId, int Quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }
            var Order = await _OrderRepository.GetOrderByUserId(user.Id);
            if(Order == null)
                Order = _OrderRepository.MakeOrderForUser(user.Id);

            // Check if the product is already in the Order
            bool productExists = await _OrderItemRepository.isProductExistsOnOrderAsync(ProductId,Order.Id);

            if (!productExists)
            {
                // Create a new Order item and associate it with the Order and product
                OrderItem OrderItem = new OrderItem
                {
                    OrderId = Order.Id,
                    ProductId = ProductId,
                    Quantity = Quantity
                };

                _OrderItemRepository.Add(OrderItem);
            }

            // Redirect to the user's Order page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromOrder(int OrderItemId)
        {
            // Find the Order item in the database
            OrderItem OrderItem = await _OrderItemRepository.GetOrderItemById(OrderItemId);

            if (OrderItem != null)
            {
                _OrderItemRepository.Remove(OrderItem);
            }

            // Redirect to the user's Order page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearOrderAsync(int OrderID){
            await _OrderRepository.ClearOrderItems(OrderID);
            return RedirectToAction("Index");
        }
    }
}