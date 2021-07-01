using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebStore.Servicess.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            //return Content("Тест контроллера!");
            var products = ProductData
            .GetProducts()
            .Take(6)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImgUrl = p.ImgUrl,
            });

            ViewBag.Products = products;
            return View();
        }

        public IActionResult SecondAction()
        {
            return Content(_Configuration["Creatings"]);
        }

        public IActionResult Error404() => View();
        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult Checkout() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();
    }
}
