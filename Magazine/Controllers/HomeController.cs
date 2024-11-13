using Magazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Magazine.Controllers
{
    public class HomeController : Controller
    {
        private readonly MagazineDbContext db = new MagazineDbContext();

        // GET: Home
        public ActionResult Index()
        {
            var articles = db.Articles.Include("Category").ToList();
            ViewBag.CategoryID = null;
            return View(articles);
        }

        // GET: Category
        public ActionResult Category(int CategoryID)
        {
            ViewBag.CategoryID = CategoryID;
            var articles = db.Articles.Include("Category").Where(a => a.CategoryID == CategoryID).ToList();
            return View(articles);
        }
    }
}