using Magazine.Models;
using System;
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
            return View(articles);
        }
    }
}