using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magazine.Models;

namespace Magazine.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly MagazineDbContext db = new MagazineDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = db.Categories.Include("SubCategories").ToList();
            return View(categories);
        }

        // GET: Admin/Category/AddCategory
        public ActionResult AddCategory()
        {
            return View();
        }

        // POST: Admin/Category/AddCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult AddSubcategory(int categoryId)
        {
            ViewBag.CategoryID = categoryId;
            return View(new SubCategory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubcategory(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subCategory);
        }

        // GET: Admin/Category/EditCategory/5
        public ActionResult EditCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(int id, Category updatedCategory)
        {
            if (ModelState.IsValid)
            {
                var category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }

                category.CategoryName = updatedCategory.CategoryName;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(updatedCategory);
        }

        // GET: Admin/Category/EditSubCategory/5 (Edit Subcategory)
        public ActionResult EditSubCategory(int id)
        {
            var subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: Admin/Category/EditSubCategory/5 (Edit Subcategory)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(int id, SubCategory updatedSubCategory)
        {
            if (ModelState.IsValid)
            {
                var subCategory = db.SubCategories.Find(id);
                if (subCategory == null)
                {
                    return HttpNotFound();
                }

                subCategory.SubCategoryName = updatedSubCategory.SubCategoryName;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedSubCategory);
        }

        // GET: Admin/Category/DeleteCategory/5
        public ActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Include("SubCategories").FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/DeleteCategory/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            var category = db.Categories.Include("SubCategories").FirstOrDefault(c => c.CategoryID == id);
            if (category != null)
            {
                // Xóa các chuyên mục con nếu có
                foreach (var subCategory in category.SubCategories.ToList())
                {
                    db.SubCategories.Remove(subCategory);
                }

                // Xóa chuyên mục cha
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Category/DeleteSubCategory/5 (Delete Subcategory)
        public ActionResult DeleteSubCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: Admin/Category/DeleteSubCategory/5 (Delete Subcategory)
        [HttpPost, ActionName("DeleteSubCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubCategoryConfirmed(int id)
        {
            var subCategory = db.SubCategories.Find(id);
            if (subCategory != null)
            {
                db.SubCategories.Remove(subCategory);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
