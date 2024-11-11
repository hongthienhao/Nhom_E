using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories;

public partial class OrderDetail
{
    [Key]
    [Column("order_detail_id")]
    public int OrderDetailId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("koi_fish_type")]
    [StringLength(50)]
    public string? KoiFishType { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("price_per_unit", TypeName = "decimal(18, 2)")]
    public decimal? PricePerUnit { get; set; }

    [Column("total_price", TypeName = "decimal(18, 2)")]
    public decimal? TotalPrice { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;
}
