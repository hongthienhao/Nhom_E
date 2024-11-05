using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    [Column("feedback_id")]
    public int FeedbackId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("resolved")]
    public bool? Resolved { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("Feedbacks")]
    public virtual Service Service { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Feedbacks")]
    public virtual User User { get; set; } = null!;
}
