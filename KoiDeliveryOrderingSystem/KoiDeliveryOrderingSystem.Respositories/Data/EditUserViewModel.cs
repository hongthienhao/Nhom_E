namespace KoiDeliveryOrderingSystem.Repositories
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty; // Đảm bảo không null
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
