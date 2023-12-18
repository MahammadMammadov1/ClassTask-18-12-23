using Agency.Business.Services.Interfaces;
using Agency.Core.Models;
using Agency.Core.Repositories.Interfaces;
using Agency.Data.DAL;
using Agency.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Implementations
{
    public class CatagoryService : ICatagoryService
    {
        private readonly ICatagoryRepository _catagoryRepository;

        public CatagoryService(ICatagoryRepository catagoryRepository )
        {
            this._catagoryRepository = catagoryRepository;
        }

        public async Task CreateAsync(Catagory entity)
        {
            if (_catagoryRepository.Table.Any(x => x.Name == entity.Name))
                throw new NullReferenceException();
            await _catagoryRepository.CreateAsync(entity);
            await _catagoryRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var result =  await _catagoryRepository.GetByIdAsync(x=>x.Id == id);
            if (result == null) throw new NullReferenceException();

            _catagoryRepository.DeleteAsync(result);
            await _catagoryRepository.CommitAsync();

        }

        public Task<List<Catagory>> GetAllAsync()
        {
            return _catagoryRepository.GetAllAsync();
        }

        public Task<Catagory> GetByIdAsync(int id)
        {
            return _catagoryRepository.GetByIdAsync(x => x.Id == id);   
        }

        public async Task UpdateAsync(Catagory entity)
        {
            var cat = await _catagoryRepository.GetByIdAsync(x=>x.Id == entity.Id);
            if (cat == null) throw new NullReferenceException();

            if (_catagoryRepository.Table.Any(x => x.Name == entity.Name && cat.Id != entity.Id))
                throw new NullReferenceException();
            cat.Name = entity.Name;
            await _catagoryRepository.CommitAsync();
        }
    }
}
