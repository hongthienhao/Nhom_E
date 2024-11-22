using System.ComponentModel.DataAnnotations;

namespace KoiDeliveryOrderingSystem.Repositories
{
    public class EditCustomerViewModel
    {
        [Required]
        public int UserId { get; set; } // ID khách hàng, không hiển thị nhưng cần gửi về server

        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên không được vượt quá 255 ký tự.")]
        public string Name { get; set; } // Tên khách hàng

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [StringLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự.")]
        public string Email { get; set; } // Email khách hàng

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string Phone { get; set; } // Số điện thoại khách hàng

        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự.")]
        public string Address { get; set; } // Địa chỉ khách hàng
    }
}
