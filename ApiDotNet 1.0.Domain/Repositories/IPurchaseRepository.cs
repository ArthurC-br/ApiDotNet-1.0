using ApiDotNet_1._0.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet_1._0.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetByIdAsync(int id);
        Task<Purchase> CreateAsync(Purchase purchase);
        Task<ICollection<Purchase>> GetAllAsync();
        Task DeleteAsync(Purchase purchase);
        Task EditAsync(Purchase purchase);
    }
}
