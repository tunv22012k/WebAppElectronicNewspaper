using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazine.Models
{
    public class Comment
    {
        [Key]
        public int CmtID { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Article")]
        public int ArticleID { get; set; }
        public Article Article { get; set; }

        [Required]
        public string CmtContent { get; set; }

        public DateTime DateComment { get; set; } = DateTime.Now;
    }
}