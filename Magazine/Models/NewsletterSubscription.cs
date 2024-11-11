using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazine.Models
{
    public class NewsletterSubscription
    {
        [Key]
        public int SubID { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        public DateTime SubDate { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User User { get; set; }
    }
}