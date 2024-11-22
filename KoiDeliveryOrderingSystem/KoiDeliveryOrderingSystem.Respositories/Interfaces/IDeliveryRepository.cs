using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetAllDeliveriesAsync();
        Task<Delivery> GetDeliveryByIdAsync(int id);
        Task AddDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(int id);
    }
}
