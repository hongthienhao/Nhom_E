using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public string? KoiFishType { get; set; }

    public int? Quantity { get; set; }

    public double? Weight { get; set; }

    public virtual Order Order { get; set; } = null!;
}
