using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magazine.Areas.Admin.ViewModels;
using Magazine.Models;
using System.Security.Cryptography;
using System.Text;

namespace Magazine.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private MagazineDbContext db = new MagazineDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            List<Account> accounts = db.Accounts.ToList();
            return View();
        }
      
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hash password nhập vào để so sánh
                string hashedPassword = HashPassword(model.PasswordHash);

                // Kiểm tra username và passwordhash từ database
                var account = db.Accounts.FirstOrDefault(a => a.Username == model.Username && a.PasswordHash == hashedPassword);

                if (account != null)
                {
                    return RedirectToAction("Index", "HomeAdmin");
                }

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            return View(model);
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}