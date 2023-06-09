using Microsoft.AspNetCore.Mvc;


namespace e_commerce_store.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}