using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories;

public partial class Order
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("pickup_location")]
    [StringLength(255)]
    public string? PickupLocation { get; set; }

    [Column("delivery_location")]
    [StringLength(255)]
    public string? DeliveryLocation { get; set; }

    [Column("shipping_method")]
    [StringLength(50)]
    public string ShippingMethod { get; set; } = null!;

    [Column("total_weight")]
    public double? TotalWeight { get; set; }

    [Column("total_quantity")]
    public int? TotalQuantity { get; set; }

    [Column("additional_services")]
    [StringLength(255)]
    public string? AdditionalServices { get; set; }

    [Column("order_date", TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [Column("estimated_delivery_date", TypeName = "datetime")]
    public DateTime? EstimatedDeliveryDate { get; set; }

    [Column("status")]
    [StringLength(50)]
    public string Status { get; set; } = null!;

    [Column("payment_method")]
    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [Column("total_price", TypeName = "decimal(18, 2)")]
    public decimal? TotalPrice { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual User Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    [InverseProperty("Order")]
    public virtual ICollection<DeliveryStatusHistory> DeliveryStatusHistories { get; set; } = new List<DeliveryStatusHistory>();

    [InverseProperty("Order")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("Order")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
