using e_commerce_store.data;
using e_commerce_store.Models;
using e_commerce_store.Models.Interfaces;
using e_commerce_store.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace e_commerce_store.Controllers
{
   [Authorize(Policy = "RequireAdministratorRole")]
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
            return RedirectToAction("Edit");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id){
            var order = await _OrderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public async Task<IActionResult> Edit(int id) {
            var status = new OrderEditStatus{
                Id = id
            };
            return View(status);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditStatus EditStatus) {
            if (ModelState.IsValid)
            {
                var order = await _OrderRepository.GetOrderById(EditStatus.Id);
                if(EditStatus.status != null)
                {
                    order.Status = EditStatus.status.ToString();
                    _OrderRepository.Update(order);
                }
                return RedirectToAction("Orders", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(EditStatus);
        }

        
        [HttpPost]
        public async Task Delete(int id){
            var order = await _OrderRepository.GetOrderById(id);
            await _OrderRepository.ClearOrderItems(id);
            _OrderRepository.Remove(order);
        }

        [HttpPost]
        public async Task<IActionResult> ClearOrderAsync(int OrderID){
            await _OrderRepository.ClearOrderItems(OrderID);
            return RedirectToAction("Index");
        }
    }
}