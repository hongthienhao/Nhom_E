namespace KoiDeliveryOrderingSystem.Repositories
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>(); // Danh sách đơn hàng của người dùng
    }

    // Đơn hàng người dùng
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
        public string ShippingMethod { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
