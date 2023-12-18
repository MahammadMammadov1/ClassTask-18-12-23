using Agency.Business.Exceptions;
using Agency.Business.Services.Interfaces;
using Agency.Core.Models;
using Agency.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }
        public async Task CreateAsync(Portfolio entity)
        {
            string fileName = "";
            if (entity.FormFile != null)
            {
                fileName = entity.FormFile.FileName;
                if (entity.FormFile.ContentType != "image/jpeg" && entity.FormFile.ContentType != "image/png")
                {
                    throw new TotalPortfolioException("FormFile", " png or jpeg files");
                }

                if (entity.FormFile.Length > 1000000)
                {
                    throw new TotalPortfolioException("FormFile", "< 1 MB");
                }

                if (entity.FormFile.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\II Novbe\\Desktop\\TasksCode\\WebApplication4\\WebApplication4\\wwwroot\\uploads\\portfolio\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    entity.FormFile.CopyTo(fileStream);
                }
                entity.ImageUrl = fileName;
            }
            else
            {
                throw new TotalPortfolioException("FormFile", " required");

            }

            await _portfolioRepository.CreateAsync(entity);
            await _portfolioRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var resilt =await _portfolioRepository.GetByIdAsync(x => x.Id == id);
            if (resilt != null)
            {
                 _portfolioRepository.DeleteAsync(resilt);
                await _portfolioRepository.CommitAsync();

            }
        }

        public async Task<List<Portfolio>> GetAllAsync()
        {
            return await _portfolioRepository.GetAllAsync();
        }

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            return await _portfolioRepository.GetByIdAsync(x=>x.Id == id);
        }

        public async Task UpdateAsync(Portfolio entity)
        {
            var wanted = await _portfolioRepository.GetByIdAsync(x => x.Id == entity.Id);


            string oldFilePath = "C:\\Users\\II Novbe\\Desktop\\TasksCode\\WebApplication4\\WebApplication4\\wwwroot\\uploads\\portfolio\\" + wanted.ImageUrl;

            if (entity.FormFile != null)
            {

                string newFileName = entity.FormFile.FileName;
                if (entity.FormFile.ContentType != "image/jpeg" && entity.FormFile.ContentType != "image/png")
                {
                    throw new TotalPortfolioException("FormFile", " png or jpeg files");
                }

                if (entity.FormFile.Length > 1048576)
                {
                    throw new TotalPortfolioException("FormFile", " < 1 Mb");
                }

                if (entity.FormFile.FileName.Length > 64)
                {
                    newFileName = newFileName.Substring(newFileName.Length - 64, 64);
                }

                newFileName = Guid.NewGuid().ToString() + newFileName;

                string newFilePath = "C:\\Users\\II Novbe\\Desktop\\Pustok-Last-version\\Pustok\\wwwroot\\uploads\\sliders\\" + newFileName;
                using (FileStream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    entity.FormFile.CopyTo(fileStream);
                }

                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                wanted.ImageUrl = newFileName;
            }

            wanted.Title = entity.Title;
            wanted.Description = entity.Description;
            wanted.ImageUrl = entity.ImageUrl;
            await  _portfolioRepository.CommitAsync();
            }

    }




       

        
} 

