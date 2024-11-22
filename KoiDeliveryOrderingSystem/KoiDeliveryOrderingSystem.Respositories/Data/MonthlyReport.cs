namespace KoiDeliveryOrderingSystem.Repositories
{
    public class MonthlyReport
    {
        public int Year { get; set; }  // Năm báo cáo
        public int Month { get; set; }  // Tháng báo cáo
        public decimal Revenue { get; set; }  // Doanh thu trong tháng
        public int TotalOrders { get; set; }  // Tổng số đơn hàng
        public int DeliveredOrders { get; set; }  // Số đơn hàng đã giao
        public int PendingOrders { get; set; }  // Số đơn hàng đang chờ
        public int ShippedOrders { get; set; }  // Số đơn hàng đang giao (Shipped)
    }
}
