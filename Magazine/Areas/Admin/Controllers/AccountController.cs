using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magazine.Areas.Admin.ViewModels;
using Magazine.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

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
                    // Tạo ticket FormsAuthentication với vai trò người dùng
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,                                      // Version
                        account.Username,                       // User name
                        DateTime.Now,                           // Issue date
                        DateTime.Now.AddMinutes(30),            // Expiration
                        false,                                  // Persistent
                        $"Admin|{account.Username}"             // User's
                    );

                    // Mã hóa ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    // Tạo cookie
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    // set info user login
                    Session["UserLogin"] = account;

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
            FormsAuthentication.SignOut();
            Session.Clear(); // Xóa tất cả dữ liệu session
            return RedirectToAction("Login");
        }
    }
}