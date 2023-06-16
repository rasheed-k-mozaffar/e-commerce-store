using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
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

        public CartController(UserManager<AppUser> userManager,ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
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
        public async Task<IActionResult> AddToCart(int ProductId, int Quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }
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

            // Redirect to the user's cart page
            return RedirectToAction("Index");
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
    }
}