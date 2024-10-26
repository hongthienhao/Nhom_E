namespace KoiDeliveryOrderingSystem.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
        public User User { get; set; }
    }
}
