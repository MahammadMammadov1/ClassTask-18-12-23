using Agency.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Interfaces
{
    public interface ICatagoryService
    {
        Task CreateAsync(Catagory entity);
        Task UpdateAsync(Catagory entity);
        Task Delete(int id);
        Task<Catagory> GetByIdAsync(int id);
        Task<List<Catagory>> GetAllAsync();
    }
}
