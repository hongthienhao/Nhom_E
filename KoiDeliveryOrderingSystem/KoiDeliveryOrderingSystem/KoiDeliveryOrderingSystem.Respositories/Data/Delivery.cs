using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int OrderId { get; set; }

    public int DeliveryStaffId { get; set; }

    public string DeliveryStatus { get; set; } = null!;

    public DateTime? EstimatedTime { get; set; }

    public DateTime? ActualTime { get; set; }

    public string? DeliveryAddress { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User DeliveryStaff { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
