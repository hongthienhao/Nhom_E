namespace KoiDeliveryOrderingSystem.Repositories
{
    public class MonthlyReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public int TotalOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int PendingOrders { get; set; }
    }
}
