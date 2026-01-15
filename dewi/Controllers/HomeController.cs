using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace dewi.Controllers
{
    public class HomeController : Controller
    {

       

        public IActionResult Index()
        {
            return View();
        }

    
    }
}
