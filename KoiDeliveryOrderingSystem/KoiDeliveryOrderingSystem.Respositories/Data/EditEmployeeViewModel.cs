using System.ComponentModel.DataAnnotations;

namespace KoiDeliveryOrderingSystem.Repositories
{
    public class EditEmployeeViewModel
    {
        [Required]
        public int UserId { get; set; } // ID nhân viên, không hiển thị nhưng cần gửi về server

        [Required(ErrorMessage = "Tên không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên không được vượt quá 255 ký tự.")]
        public string Name { get; set; } // Tên nhân viên

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [StringLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự.")]
        public string Email { get; set; } // Email nhân viên

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string Phone { get; set; } // Số điện thoại

        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự.")]
        public string Address { get; set; } // Địa chỉ nhân viên

        [Required(ErrorMessage = "Vai trò không được để trống.")]
        public int RoleId { get; set; } // Vai trò nhân viên
    }
}
