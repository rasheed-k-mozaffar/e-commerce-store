using Microsoft.AspNetCore.Mvc;

namespace e_commerce_store.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}