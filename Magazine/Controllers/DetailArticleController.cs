using Magazine.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class DetailArticleController : Controller
    {
        private readonly MagazineDbContext db = new MagazineDbContext();

        // GET: DetailArticle
        public ActionResult Index(int ArticleID)
        {
            var article = db.Articles.Include("Category").Include("Comments.User").FirstOrDefault(c => c.ArticleID == ArticleID);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        
        [HttpPost]
        public JsonResult PostCmt(int ArticleID, string UserName, string PasswordHash, string CmtContent)
        {
            try
            {
                PasswordHash = HashPassword(PasswordHash);

                // get data user
                var account = db.Users.FirstOrDefault(a => a.Username == UserName && a.PasswordHash == PasswordHash);

                if (account != null)
                {
                    var Comment = new Comment
                    {
                        UserID = account.UserID,
                        ArticleID = ArticleID,
                        CmtContent = CmtContent
                    };

                    db.Comments.Add(Comment);
                    db.SaveChanges();

                    return Json(new { success = true, data = 1 });
                }
                else
                {
                    return Json(new { success = false, data = 0 });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = 1 });
                throw new Exception("Delete err: " + ex.Message);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}