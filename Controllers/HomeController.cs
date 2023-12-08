using DeMoGCS10035.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DeMoGCS10035.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string successMessage = TempData["Success"] as string;
            ViewData["Success"] = successMessage;
            var author = HttpContext.Session.GetString("user");
           
            if (author != null)
            {
                dynamic? lastAccessInfo;
                var accessInfoSave = new
                {
                    userName = "Demo",
                    role = "User"
                };
                lastAccessInfo = JsonConvert.DeserializeObject(author, accessInfoSave.GetType());
                if (lastAccessInfo != null && lastAccessInfo?.role != null && lastAccessInfo?.userName != null && lastAccessInfo?.role == "Admin")
                {
                    ViewData["Role"] = lastAccessInfo?.role;
                }
                ViewData["Login"] = lastAccessInfo?.userName;
            }
            else
            {
                ViewData["Login"] = null;
            }
            
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}