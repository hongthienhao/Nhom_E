namespace KoiDeliveryOrderingSystem.Repositories;
public class UserProfileViewModel
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }

    // Danh sách đơn hàng của người dùng
    public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

    // Danh sách phản hồi của người dùng
    public List<FeedbackViewModel> Feedbacks { get; set; } = new List<FeedbackViewModel>();
}

public class OrderViewModel
{
    public int OrderId { get; set; }
    public string PickupLocation { get; set; }
    public string DeliveryLocation { get; set; }
    public string ShippingMethod { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice { get; set; }

    // Liên kết phản hồi với đơn hàng (Feedback đã có thuộc tính OrderId)
    public FeedbackViewModel Feedback { get; set; }
}

public class FeedbackViewModel
{
    public int FeedbackId { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }  // Thuộc tính liên kết với đơn hàng
    public int ServiceId { get; set; } // Dịch vụ liên quan
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Resolved { get; set; }


    // Danh sách dịch vụ để chọn
    public List<KeyValuePair<int, string>> Services { get; set; } = new List<KeyValuePair<int, string>>();
}

