using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories;

[Table("DeliveryStatusHistory")]
public partial class DeliveryStatusHistory
{
    [Key]
    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

    [Column("status_date", TypeName = "datetime")]
    public DateTime? StatusDate { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("DeliveryStatusHistories")]
    public virtual Order Order { get; set; } = null!;
}
