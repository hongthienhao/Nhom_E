namespace KoiDeliveryOrderingSystem.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public int DeliveryStaffId { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public DateTime? ActualTime { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
        public User DeliveryStaff { get; set; }
    }
}
