using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazine.Models
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        [Required]
        [MaxLength]
        public string Title { get; set; }

        public string Contents { get; set; }

        public DateTime PostTime { get; set; } = DateTime.Now;

        public string ArticleImage { get; set; }

        [MaxLength(256)]
        public string Link { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public string ApprovalStatus { get; set; }
        public string Feedback { get; set; }
    }
}