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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}