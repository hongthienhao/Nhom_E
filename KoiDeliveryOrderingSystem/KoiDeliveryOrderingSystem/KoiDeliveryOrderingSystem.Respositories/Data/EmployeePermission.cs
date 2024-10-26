using System;
using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class EmployeePermission
{
    public int EmployeePermissionId { get; set; }

    public int EmployeeId { get; set; }

    public string Permission { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User Employee { get; set; } = null!;
}
