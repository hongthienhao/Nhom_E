using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories;

[Table("ServicePricing")]
public partial class ServicePricing
{
    [Key]
    [Column("pricing_id")]
    public int PricingId { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("effective_date", TypeName = "datetime")]
    public DateTime? EffectiveDate { get; set; }

    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column("conditions")]
    public string? Conditions { get; set; }

    [Column("weight_range")]
    public string WeightRange { get; set; } = "N/A"; // Giá trị mặc định nếu không được thiết lập

    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("ServicePricings")]
    public virtual Service Service { get; set; } = null!;
}
