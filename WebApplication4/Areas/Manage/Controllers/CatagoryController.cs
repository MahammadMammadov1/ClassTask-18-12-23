using Agency.Business.Services.Interfaces;
using Agency.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class CatagoryController : Controller
    {
        private readonly ICatagoryService _catagoryService;

        public CatagoryController(ICatagoryService catagoryService)
        {
            this._catagoryService = catagoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<Catagory> catagories =await _catagoryService.GetAllAsync();
            return View(catagories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Catagory catagory)
        {
            if(!ModelState.IsValid) return View();

            await _catagoryService.CreateAsync(catagory);
            return RedirectToAction("Index","Catagory");
        }
        public async Task<IActionResult> Update(int id)
        {
             var result = await _catagoryService.GetByIdAsync(id);

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Catagory catagory)
        {
            if (!ModelState.IsValid) return View();

            await _catagoryService.UpdateAsync(catagory);
            return RedirectToAction("Index", "Catagory");
        }

        public async Task<IActionResult> Delete(int id) 
        {
             await _catagoryService.Delete(id);
            return RedirectToAction("Index", "catagory");
        }
    }
}
