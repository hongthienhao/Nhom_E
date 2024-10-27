using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<EmployeePermission> EmployeePermissions { get; set; }
    }
}
