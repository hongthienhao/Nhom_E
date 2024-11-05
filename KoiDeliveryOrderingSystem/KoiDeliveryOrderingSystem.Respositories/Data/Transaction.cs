using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories;

public partial class Transaction
{
    [Key]
    [Column("transaction_id")]
    public int TransactionId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("amount", TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Transactions")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Transactions")]
    public virtual User User { get; set; } = null!;
}
