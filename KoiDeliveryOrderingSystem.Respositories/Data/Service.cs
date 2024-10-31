using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Category { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
