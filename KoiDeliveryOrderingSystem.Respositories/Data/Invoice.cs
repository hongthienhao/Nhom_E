using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public decimal? AmountDue { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
