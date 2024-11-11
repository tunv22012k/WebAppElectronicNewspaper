using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazine.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [ForeignKey("Account")]
        public int RoleID { get; set; }
        public Account Account { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [MaxLength(256)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MaxLength]
        public string PasswordHash { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string Fullname { get; set; }

        public string Avatar { get; set; }
    }
}