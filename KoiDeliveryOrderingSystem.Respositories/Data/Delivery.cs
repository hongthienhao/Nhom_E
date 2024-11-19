using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace KoiDeliveryOrderingSystem.Repositories;

[Table("Delivery")]
public partial class Delivery
{
    [Key]
    [Column("delivery_id")]
    public int DeliveryId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("delivery_staff_id")]
    public int DeliveryStaffId { get; set; }

    [Column("tracking_code")]
    [StringLength(50)]
    public string? TrackingCode { get; set; }

    [Column("delivery_status")]
    [StringLength(50)]
    public string DeliveryStatus { get; set; } = null!;

    [Column("estimated_time", TypeName = "datetime")]
    public DateTime? EstimatedTime { get; set; }

    [Column("actual_time", TypeName = "datetime")]
    public DateTime? ActualTime { get; set; }

    [Column("delivery_address")]
    [StringLength(255)]
    public string? DeliveryAddress { get; set; }

    [Column("delivery_notes")]
    public string? DeliveryNotes { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("DeliveryStaffId")]
    [InverseProperty("Deliveries")]
    public virtual User DeliveryStaff { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("Deliveries")]
    public virtual Order Order { get; set; } = null!;
}
