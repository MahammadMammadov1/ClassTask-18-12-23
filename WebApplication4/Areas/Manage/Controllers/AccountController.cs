using Agency.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Areas.Manage.ViewModels;

namespace WebApplication4.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = null;
            user = await _userManager.FindByEmailAsync(adminLoginVM.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVM.Password,false,false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser user = null;

            user = new AppUser
            {
                UserName = "SuperAdmin",
                Email = "mehemmedmemmedov240@gmail.com",
                FullName = "admin", 

            };
            var result =await _userManager.CreateAsync(user,"Admin123@");

            return Ok("created");
        }



        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role = new IdentityRole("SuperAdmin");
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role);

            return Ok("Created");
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        

       
        public async Task<IActionResult> AddRoleAdmin()
        {
            AppUser superAdmin = await _userManager.FindByEmailAsync("mehemmedmemmedov240@gmail.com");

            await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            return Ok("succesfully added");
        }



    }
}
