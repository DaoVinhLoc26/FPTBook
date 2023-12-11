using DeMoGCS10035.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using System.Data;
using static DeMoGCS10035.Helper;

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
        [Route("category/edit/{id}")]
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = db.Categories.FirstOrDefault(category => category.Id.Equals(id));
            return View(category);
        }
        [Route("category/edit/{id}")]
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            var cat = db.Categories.FirstOrDefault(category => category.Id.Equals(category.Id));
            if (cat != null)
            {
                cat.Name = category.Name;
                cat.Details = category.Details;
                db.SaveChanges();
            }
            return RedirectToAction("Category");
        }
        [Route("category/delete/{id}")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int idTemp = int.Parse(id.ToString());
            var cat = db.Categories.FirstOrDefault(category => category.Id.Equals(idTemp));
            if (cat != null)
            {
                var booksToDelete = db.Books.Where(book => book.CatId == idTemp);
                db.Books.RemoveRange(booksToDelete);
                db.SaveChanges();
                db.Remove(cat);
                db.SaveChanges();
            }
            return Redirect("Category");
        }
        [Route("product")]
        public IActionResult Product()
        {
            var listProd = db.Books.ToList();
            return View(listProd);
        }

        [Route("product/add")]
        [HttpGet]
        public IActionResult AddBook()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            productViewModel.Authors = new SelectList(db.Authors.ToList(), "Id", "Name");
            productViewModel.Publishers = new SelectList(db.Publishers.ToList(), "Id", "Name");
            return View(productViewModel);
        }
        [Route("product/add")]
        [HttpPost]
        public IActionResult AddBook(Book book, IFormFile image)
        {
            // Check if the model is valid


            if (image != null && image.Length > 0)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", Guid.NewGuid().ToString() + Path.GetExtension(image.FileName));

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // Lưu tên hình ảnh vào thuộc tính của Book
                book.Thumb = Path.GetFileName(imagePath);
            }
            db.Add(book);
            db.SaveChanges();
            return RedirectToAction("Product");
            // If the model is not valid, redisplay the form with validation errors
            // Also, make sure to repopulate the Categories property in case of errors
        }
        [Route("product/edit/{id}")]
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            // Lấy thông tin sách cần sửa
            var book = db.Books.FirstOrDefault(b => b.Id.Equals(id));

            if (book == null)
            {
                // Trả về 404 Not Found nếu không tìm thấy sách
                return NotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel
            {
                Book = book,
                Categories = new SelectList(db.Categories.ToList(), "Id", "Name"),
                Authors = new SelectList(db.Authors.ToList(), "Id", "Name"),
                Publishers = new SelectList(db.Publishers.ToList(), "Id", "Name")
            };

            return View(productViewModel);
        }
        [Route("product/edit/{id}")]
        [HttpPost]
        public IActionResult EditProduct(ProductViewModel editedBook, IFormFile image)
        {
            var product = db.Books.FirstOrDefault(book => book.Id.Equals(editedBook.Book.Id));
            if (product != null)
            {
                product.Title = editedBook.Book.Title;
                product.AuthorId = editedBook.Book.AuthorId;
                product.Price = editedBook.Book.Price;
                product.PublisherId = editedBook.Book.PublisherId;
                product.Detailes = editedBook.Book.Detailes;
                if (image != null && image.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", Guid.NewGuid().ToString() + Path.GetExtension(image.FileName));

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    // Lưu tên hình ảnh vào thuộc tính của Book
                    product.Thumb = Path.GetFileName(imagePath);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Product");
        }
        [Route("product/delete/{id}")]
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            int idTemp = int.Parse(id.ToString());
            var product = db.Books.FirstOrDefault(product => product.Id.Equals(idTemp));
            if (product != null)
            {
                db.SaveChanges();
                db.Remove(product);
                db.SaveChanges();
            }
            return Redirect("Book");
        }
        [Route("user")]
        [HttpGet]
        public IActionResult UserList()
        {
            var listUser = db.Users.ToList();
            return View(listUser);
        }
        [Route("user/add")]
        [HttpGet]
        public IActionResult AddUser()
        {
            List<string> roles = new List<string> { "User", "Admin" };

            // Truyền danh sách giá trị thông qua ViewBag
            ViewBag.Roles = new SelectList(roles);
            return View();
        }
        [Route("user/add")]
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            List<string> roles = new List<string> { "User", "Admin" };
            ViewBag.Roles = new SelectList(roles);
            var u = db.Users.FirstOrDefault(x => x.Username.Equals(user.Username));
            if (u == null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            ViewBag.ErrorMessage = "This account is already exist";
            return View();
        }
        [Route("user/edit/{id}")]
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            List<string> roles = new List<string> { "User", "Admin" };
            ViewBag.Roles = new SelectList(roles);
            var user = db.Users.FirstOrDefault(b => b.Id.Equals(id));
            return View(user);


        }
        [Route("user/edit/{id}")]
        [HttpPost]
        public IActionResult EditUser(User userEd)
        {
            var user = db.Users.FirstOrDefault(u => u.Id.Equals(userEd.Id));
            if (user != null)
            {
                user.FullName = userEd.FullName;
                user.Username = userEd.Username;
                user.Password = userEd.Password;
                user.Role = userEd.Role;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            List<string> roles = new List<string> { "User", "Admin" };
            ViewBag.Roles = new SelectList(roles);
            return View();
        }
        [Route("user/delete/{id}")]
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            int idTemp = int.Parse(id.ToString());
            var user = db.Users.FirstOrDefault(product => product.Id.Equals(idTemp));
            if (user != null)
            {
                db.Remove(user);
                db.SaveChanges();
            }
            return Redirect("UserList");
        }
    }
}