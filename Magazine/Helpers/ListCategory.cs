using Magazine.Models;
using System.Collections.Generic;
using System.Linq;

namespace Magazine.Helpers
{
    public static class ListCategory
    {
        public static List<Category> GetListCategory()
        {
            using (var db = new MagazineDbContext())
            {
                return db.Categories.ToList();
            }
        }
    }
}