using DeMoGCS10035.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeMoGCS10035.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/home")]
    public class HomeAdminController : Controller
    {
        FptbookdbContext db = new FptbookdbContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var author = HttpContext.Session.GetString("user");
            if (author == null)
            {
                return Redirect("/Login");
            }
            dynamic? lastAccessInfo;
            var accessInfoSave = new
            {
                userName = "Demo",
                role = "User"
            };
            lastAccessInfo = JsonConvert.DeserializeObject(author, accessInfoSave.GetType());
            if (lastAccessInfo != null && lastAccessInfo?.role != null && lastAccessInfo?.userName != null && lastAccessInfo?.role == "Admin")
            {
                ViewData["AdminName"] = lastAccessInfo?.userName;
                if (lastAccessInfo?.role == "Admin")
                {
                    ViewData["Role"] = "Admin";
                }
                return View();
            }
            return Redirect("/");
        }
        [Route("category")]
        public IActionResult Category()
        {
            var lstCate = db.Categories.ToList();
            var author = HttpContext.Session.GetString("user");
            if (author == null)
            {
                return Redirect("/Login");
            }
            dynamic? lastAccessInfo;
            var accessInfoSave = new
            {
                userName = "Demo",
                role = "User"
            };
            lastAccessInfo = JsonConvert.DeserializeObject(author, accessInfoSave.GetType());
            if (lastAccessInfo != null && lastAccessInfo?.role != null && lastAccessInfo?.userName != null && lastAccessInfo?.role == "Admin")
            {
                ViewData["AdminName"] = lastAccessInfo?.userName;
                if (lastAccessInfo?.role == "Admin")
                {
                    ViewData["Role"] = "Admin";
                    return View(lstCate);
                }
            }
            return Redirect("/");
        }
        [Route("category/add")]
        [HttpGet]
        public IActionResult AddCategory()
        {
            var author = HttpContext.Session.GetString("user");
            if (author == null)
            {
                return Redirect("/Login");
            }
            dynamic? lastAccessInfo;
            var accessInfoSave = new
            {
                userName = "Demo",
                role = "User"
            };
            lastAccessInfo = JsonConvert.DeserializeObject(author, accessInfoSave.GetType());
            if (lastAccessInfo != null && lastAccessInfo?.role != null && lastAccessInfo?.userName != null && lastAccessInfo?.role == "Admin")
            {
                ViewData["AdminName"] = lastAccessInfo?.userName;
                if (lastAccessInfo?.role == "Admin")
                {
                    ViewData["Role"] = "Admin";
                    return View();
                }
            }
            return Redirect("/");
        }
        [Route("category/add")]
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            db.Categories.Add(category);
            Console.WriteLine("Category name" + category.Details);
            db.SaveChanges();
            return RedirectToAction("Category"); 
        }
        [Route("category/edit")]
        public IActionResult EditCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                return View (category);
            }
        }
        //Edit , Delete Category

    }
}
