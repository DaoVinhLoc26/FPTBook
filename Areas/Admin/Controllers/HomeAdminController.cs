using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeMoGCS10035.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/home")]
    public class HomeAdminController : Controller
    {
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
               if(lastAccessInfo != null && lastAccessInfo?.role != null && lastAccessInfo?.userName != null && lastAccessInfo?.role=="Admin")
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
    }
}
