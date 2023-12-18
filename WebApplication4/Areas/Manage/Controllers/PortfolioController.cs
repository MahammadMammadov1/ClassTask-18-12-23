using Agency.Business.Services.Interfaces;
using Agency.Core.Models;
using Agency.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ICatagoryService _catagoryService;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(IPortfolioService portfolioService,ICatagoryService catagoryService )
        {
            this._portfolioService = portfolioService;
            this._catagoryService = catagoryService;
        }
       

        public async Task<IActionResult> Index()
        {
            List<Portfolio> portfolios = await _portfolioService.GetAllAsync();
            return View(portfolios);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _catagoryService.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Portfolio entity)
        {
            ViewBag.Categories =await _catagoryService.GetAllAsync();
            if (!ModelState.IsValid) return View();

            await _portfolioService.CreateAsync(entity);
            return RedirectToAction("Index", "Portfolio");
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Portfolio = await _portfolioService.GetAllAsync();
            var result = await _portfolioService.GetByIdAsync(id);

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Portfolio entity)
        {
            ViewBag.Portfolio = await _portfolioService.GetAllAsync();
            if (!ModelState.IsValid) return View();

            await _portfolioService.UpdateAsync(entity);
            return RedirectToAction("Index", "Portfolio");
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Portfolio = await _portfolioService.GetAllAsync();
            await _portfolioService.Delete(id);
            return RedirectToAction("Index", "Portfolio");
        }
    }
}
