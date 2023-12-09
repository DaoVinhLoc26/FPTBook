using DeMoGCS10035.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeMoGCS10035.Controllers
{
    public class AuthController : Controller
    {
        FptbookdbContext db = new FptbookdbContext();

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                var loggedInUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (loggedInUser != null)
                {
                    var accessInfoSave = new
                    {
                        userName = loggedInUser.FullName,
                        role = loggedInUser.Role
                    };
                    string jsonSave = JsonConvert.SerializeObject(accessInfoSave);
                    HttpContext.Session.SetString("user", jsonSave);
                    TempData["Success"] = "Login Successfully";
                    Console.Write(jsonSave);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "Username or password is not correct";
                    return View();
                }
            }
            return RedirectToAction("Home", "Index");
        }
        [HttpGet]
        [Route("signup")]
        public IActionResult SignUp()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(User user)
        {
            var u = db.Users.FirstOrDefault(x => x.Username.Equals(user.Username));
            if (u == null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                TempData["Success"] = "Regist Successfully";
                return Redirect("Login");
            }

            ViewBag.ErrorMessage = "This account is already exist";
            return View("SignUp");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }
    }
}
