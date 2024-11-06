using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public string? PickupLocation { get; set; }

    public string? DeliveryLocation { get; set; }

    public string ShippingMethod { get; set; } = null!;

    public double? TotalWeight { get; set; }

    public int? TotalQuantity { get; set; }

    public string? AdditionalServices { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
