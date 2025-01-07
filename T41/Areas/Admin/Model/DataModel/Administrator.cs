using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên người dùng")]
        [StringLength(64, ErrorMessage = "Tên đăng nhập trong khoản 3-63 ký tự", MinimumLength =3)]
        [Column(TypeName ="varchar")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName{ get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [MaxLength(256)]
        [Display(Name ="Mật khẩu")]
        [Column(TypeName ="varchar")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Hãy nhập họ và tên")]
        [MaxLength(256)]
        [Display(Name = "Họ và tên")]        
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage ="Email không đúng định dạng")]
        [MaxLength(256)]
        [Display(Name = "Email")]
        [Column(TypeName = "varchar")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [StringLength(64, ErrorMessage = "Tên đăng nhập trong khoản 3-63 ký tự", MinimumLength = 3)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Là quản trị")]
        public byte? IsAdmin { get; set; }

        [Display(Name = "Kích hoạt")]
        public byte? Active { get; set; }

        [Display(Name = "Nhóm chức năng")]
        public int Role { get; set; }

        // Navigation: liên kêt, tham chiếu
        public ICollection<GrantPermission> GrantPermission { get; set; }
    }
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.", MinimumLength = 6)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Hãy xác nhận mật khẩu mới")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
    }

}