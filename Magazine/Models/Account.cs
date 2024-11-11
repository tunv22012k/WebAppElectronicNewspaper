using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Magazine.Models
{
    public class Account
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [MaxLength]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MaxLength]
        public string PasswordHash { get; set; }
        public string Avatar { get; set; }
    }
}