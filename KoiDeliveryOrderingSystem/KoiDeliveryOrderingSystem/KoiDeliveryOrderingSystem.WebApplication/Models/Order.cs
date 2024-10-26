using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
        public string ShippingMethod { get; set; }
        public float TotalWeight { get; set; }
        public int TotalQuantity { get; set; }
        public string AdditionalServices { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public User Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
