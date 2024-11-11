using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Magazine.Models
{
    public class SubCategory
    {
        [Key]
        [Required]
        public int SubCategoryID { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        [MaxLength]
        public string SubCategoryName { get; set; }
        public Category Category { get; set; }
    }
}