using DeMoGCS10035.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using X.PagedList;

namespace DeMoGCS10035.Controllers
{
    public class HomeController : Controller
    {
        FptbookdbContext db = new FptbookdbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listprod = db.Books.AsNoTracking().OrderBy(x => x.Id);
            PagedList<Book> prodList = new PagedList<Book>(listprod,pageNumber,pageSize);
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
                if (lastAccessInfo != null && lastAccessInfo?.role != null && lastAccessInfo?.userName != null && (lastAccessInfo?.role == "Admin"|| lastAccessInfo?.role == "Store Owner"))
                {
                    ViewData["Role"] = lastAccessInfo?.role;
                }
                ViewData["Login"] = lastAccessInfo?.userName;
            }
            else
            {
                ViewData["Login"] = null;
            }
            return View(prodList);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ProductWithCategory(string cateid)
        {
            List<Book> listprod = db.Books.Where(x => x.CatId.Equals(int.Parse(cateid))).ToList();
            foreach(var pro in listprod)
            {
                Console.WriteLine($"{pro.Id}, {pro.Title}");
            }
            Console.WriteLine("1");
            return View(listprod);
        }

        [Route("product/{id}")]
        public IActionResult Detail(int id)
        {
            var productDetail = db.Books.FirstOrDefault(x => x.Id.Equals(Convert.ToInt32(id)));
            return View(productDetail);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}