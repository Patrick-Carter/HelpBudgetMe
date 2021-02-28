using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;

        public AccountController(ApplicationDBContext db, UserManager<User> userManager, SignInManager<User> signinManager)
        {
            _db = db;
            _userManager = userManager;
            _signinManager = signinManager;

        }

        // GET: /Account/Signin
        
        public IActionResult Signin()
        {
          
            return View();
        }

        // GET: /Account/Signup

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // POST: /Account/Logout

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Signin", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // POST: /Account/Siginin

        public async Task<IActionResult> Signin(SigninViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                   
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "invalid credintials");
            }
            return View(model);
        }

        // POST: /Account/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signinManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
               
            }
            return View(model);
        }
    }
}
