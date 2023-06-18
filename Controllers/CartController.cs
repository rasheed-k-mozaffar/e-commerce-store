using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
using e_commerce_store.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public CartController(UserManager<AppUser> userManager,ICartRepository cartRepository, ICartItemRepository cartItemRepository,IOrderRepository orderRepository,IOrderItemRepository orderItemRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }
            var cart = await _cartRepository.GetCartByUserId(user.Id);
            if(cart == null)
                cart = _cartRepository.MakeCartForUser(user.Id);

            return View(cart);
        }

        [HttpPost]
        public async Task AddToCart(int ProductId, int Quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var cart = await _cartRepository.GetCartByUserId(user.Id);
                if(cart == null)
                    cart = _cartRepository.MakeCartForUser(user.Id);

                // Check if the product is already in the cart
                bool productExists = await _cartItemRepository.isProductExistsOnCartAsync(ProductId,cart.Id);

                if (!productExists)
                {
                    // Create a new cart item and associate it with the cart and product
                    CartItem cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = ProductId,
                        Quantity = Quantity
                    };

                    _cartItemRepository.Add(cartItem);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            // Find the cart item in the database
            CartItem cartItem = await _cartItemRepository.GetCartItemById(cartItemId);

            if (cartItem != null)
            {
                _cartItemRepository.Remove(cartItem);
            }

            // Redirect to the user's cart page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCartAsync(int cartID){
            await _cartRepository.ClearCartItems(cartID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(){
            var user = await _userManager.GetUserAsync(User);
            var cart = await _cartRepository.GetCartByUserId(user.Id);

            if(cart == null || cart.CartItems == null || cart.CartItems.Count < 1)
                return RedirectToAction("Index");

            var TotalPrice = await _cartRepository.GetTotalPrice(cart);
            var checkOutViewModel = new CheckOutViewModel{
                UserName = user.UserName,
                UserEmail = user.Email,
                TotalPrice = TotalPrice,
                PhoneNumber = user.PhoneNumber
            };
            return View(checkOutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckOutViewModel checkOutViewModel){
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                // If the model is invalid, return the view with the validation errors
                return View(checkOutViewModel);
            }
            var user = await _userManager.GetUserAsync(User);
            Cart cart = await _cartRepository.GetCartByUserId(user.Id);

            // Calculate the total cost of the order based on the cart items
            decimal totalPrice = await _cartRepository.GetTotalPrice(cart); 

            // Create a new order
            Order order = new Order
            {
                AppUserId = user.Id,
                FirstName = checkOutViewModel.FirstName,
                LastName = checkOutViewModel.LastName,
                PhoneNumber = checkOutViewModel.PhoneNumber,
                City = checkOutViewModel.City,
                Address = checkOutViewModel.Address1,
                Address2 = checkOutViewModel.Address2,
                TotalPrice = totalPrice,
                Status = "under review"
            };

            _orderRepository.Add(order);

            // Save the order items by associating them with the newly created order
            foreach (var cartItem in cart.CartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = order.Id, // Assuming Order has an Id property
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                };
                _orderItemRepository.Add(orderItem);
            }
            
            await _cartRepository.ClearCartItems(cart.Id);

            // Redirect the user to a thank you page or order confirmation page
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation(){
            return View("OrderConfirmation");
        }
    }
}