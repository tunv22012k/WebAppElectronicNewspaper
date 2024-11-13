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

        [Required(ErrorMessage = "Không thể để trống tiêu đề")]
        [MaxLength]
        public string Title { get; set; }

        [Required(ErrorMessage = "Không thể để trống nội dung")]
        public string Contents { get; set; }

        public DateTime PostTime { get; set; } = DateTime.Now;

        public string ArticleImage { get; set; }

        [Required(ErrorMessage = "Không thể để trống link hình ảnh")]
        [MaxLength(256)]
        public string Link { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string ApprovalStatus { get; set; }
        public string Feedback { get; set; }
    }
}