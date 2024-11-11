using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magazine.Models;
using Magazine.Areas.Admin.ViewModels;

namespace Magazine.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly MagazineDbContext db = new MagazineDbContext();
        // Hiển thị danh sách bài viết chờ phê duyệt
        public ActionResult Index()
        {
            var pendingArticles = db.Articles.ToList();

            return View(pendingArticles);
        }

        // Phê duyệt bài viết
        [HttpPost]
        public ActionResult Approve(int id)
        {
            var article = db.Articles.Find(id);
            if (article != null)
            {
                article.ApprovalStatus = "Đã Duyệt";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Từ chối bài viết
        [HttpPost]
        public ActionResult Reject(int id, string feedback)
        {
            var article = db.Articles.Find(id);
            if (article != null)
            {
                article.ApprovalStatus = "Từ chối";
                article.Feedback = feedback;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Yêu cầu chỉnh sửa bài viết
        [HttpPost]
        public ActionResult RequestEdit(int id, string feedback)
        {
            var article = db.Articles.Find(id);
            if (article != null)
            {
                article.ApprovalStatus = "Yêu cầu chỉnh sửa";
                article.Feedback = feedback;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}