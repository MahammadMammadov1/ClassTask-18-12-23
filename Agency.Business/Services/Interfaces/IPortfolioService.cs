using Agency.Core.Models;
using Agency.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task CreateAsync(Portfolio entity);
        Task UpdateAsync(Portfolio entity);
        Task Delete(int id);
        Task<Portfolio> GetByIdAsync(int id);
        Task<List<Portfolio>> GetAllAsync();
    }
}
