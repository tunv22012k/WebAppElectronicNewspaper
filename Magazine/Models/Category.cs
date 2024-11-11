using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Magazine.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Bạn bắt buộc phải nhập tên chuyên mục cha")]
        [MaxLength]
        public string CategoryName { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}