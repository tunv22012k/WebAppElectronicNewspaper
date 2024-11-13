using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magazine.Models;
using Magazine.Areas.Admin.ViewModels;
using System.Net;
using System.Data.Entity;

namespace Magazine.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly MagazineDbContext db = new MagazineDbContext();
        // Hiển thị danh sách bài viết
        public ActionResult Index()
        {
            var articles = db.Articles.Include("Category").OrderByDescending(a => a.ArticleID).ToList();

            return View(articles);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                // set data
                article.ArticleImage = article.Link;
                article.PostTime = DateTime.Now;
                article.UserID = 1;

                // store article
                db.Articles.Add(article);
                db.SaveChanges();

                
                return RedirectToAction("Index");
            }

            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }

        // GET: Article/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Article article = db.Articles.Find(id);
            if (article == null)
                return HttpNotFound();

            ViewBag.CategoryID = article.CategoryID;
            ViewBag.ListCategory = db.Categories.ToList();
            return View(article);
        }

        // POST: Article/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }

        // GET: User/Delete/${id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Article article = db.Articles.Find(id);
            if (article == null)
                return HttpNotFound();

            return View(article);
        }

        // POST: User/Delete/${id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
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