using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiDeliveryOrderingSystem.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng
        public int EmployeeId { get; set; } // ID nhân viên (tự động tăng)

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } // Tên nhân viên

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } // Email nhân viên

        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } // Số điện thoại

        [MaxLength(500)]
        public string Address { get; set; } // Địa chỉ

        public int RoleId { get; set; } // Vai trò (2: Sales, 3: Delivery)
    }
}
