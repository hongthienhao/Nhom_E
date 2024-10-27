using System;

namespace KoiDeliveryOrderingSystem.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string KoiFishType { get; set; }
        public int Quantity { get; set; }
        public float Weight { get; set; }

        public Order Order { get; set; }
    }
}
