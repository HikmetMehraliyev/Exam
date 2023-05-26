using ExamProject5.DAL;
using ExamProject5.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject5.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register) 
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var user = _mapper.Map<IdentityUser>(register);
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(register);
            }
            await _userManager.AddToRoleAsync(user, "Visitor");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            return View();
        }
        public IActionResult AddRoles()
        {
            return View();
        }
    }
}
