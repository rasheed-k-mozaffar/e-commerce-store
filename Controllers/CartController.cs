using Microsoft.AspNetCore.Mvc;

namespace e_commerce_store.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}