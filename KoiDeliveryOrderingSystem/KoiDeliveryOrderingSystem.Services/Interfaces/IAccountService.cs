using KoiDeliveryOrderingSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> LoginAsync(string email, string password);
        Task RegisterAsync(string username, string email, string password, string confirmPassword, string phone, string address);
    }
}
