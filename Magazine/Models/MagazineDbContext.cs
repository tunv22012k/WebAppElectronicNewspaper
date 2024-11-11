using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Magazine.Models
{
    public class MagazineDbContext : DbContext
    {
        //public MagazineDbContext() : base("MagazineString") { }
        public MagazineDbContext() : base("MyConnectionString") { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
    }
}