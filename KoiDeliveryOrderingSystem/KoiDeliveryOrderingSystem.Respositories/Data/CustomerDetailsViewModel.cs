using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public User Customer { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
