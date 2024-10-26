namespace KoiDeliveryOrderingSystem.Models
{
    public class EmployeePermission
    {
        public int EmployeePermissionId { get; set; }
        public int EmployeeId { get; set; }
        public string Permission { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Employee { get; set; }
    }
}
