using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    public class SavingController : Controller
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public SavingController(ApplicationDBContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string Id = _userManager.GetUserId(User);
                User currentUser = await _db.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();

                var savings = _db.Savings.Where(a => a.User == currentUser).OrderByDescending(b => b.DateCreated).Take(10).ToList();

                SavingsViewModel model = new SavingsViewModel()
                {
                    Savings = savings,
                    BudgetedForSavings = currentUser.BudgetedForNeeds
                };

                return View(model);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult AddSaving()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditSaving(int Id)
        {
            Saving saving = _db.Savings.Where(a => a.Id == Id).FirstOrDefault();

            var model = new EditViewModel()
            {
                Id = saving.Id,
                Name = saving.Name,
                Amount = saving.Amount,
                PreviousAmount = saving.Amount,
                DateCreated = saving.DateCreated
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteSaving(int Id)
        {
            Saving saving = _db.Savings.Where(a => a.Id == Id).FirstOrDefault();

            return View(saving);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSaving(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();

                    Saving saving = new Saving()
                    {
                        Name = model.Name,
                        Amount = model.Amount,
                        User = currentUser,
                        DateCreated = DateTime.Now
                    };

                    currentUser.CurrentMoney -= model.Amount;
                    currentUser.BudgetedForSavings -= model.Amount;
                    currentUser.AllTimeSpent += model.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    await _db.Savings.AddAsync(saving);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditSaving(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();
                    Saving saving = _db.Savings.Where(a => a.Id == model.Id).FirstOrDefault();


                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    currentUser.CurrentMoney += amountChanged;
                    currentUser.AllTimeSpent -= amountChanged;
                    currentUser.BudgetedForSavings += amountChanged;
                    await _userManager.UpdateAsync(currentUser);
                    saving.Amount = model.Amount;
                    saving.Name = model.Name;
                    _db.Savings.Update(saving);
                    await _db.SaveChangesAsync();


                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteSaving(Saving saving)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string UserId = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == UserId).FirstOrDefault();

                    currentUser.CurrentMoney += saving.Amount;
                    currentUser.BudgetedForSavings += saving.Amount;
                    currentUser.AllTimeSpent -= saving.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    _db.Savings.Remove(saving);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index", "Dashboard");
                }
                catch
                {
                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("api/GetMoreSaving")]

        public async Task<JsonResult> GetMorePaycheck()
        {

            // get req body and convert text to int
            string req = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            int skip = 10;

            try
            {
                skip = int.Parse(req);
            }
            catch
            {
                return Json("Something went wrong");
            }

            string Id = _userManager.GetUserId(User);

            var savings = _db.Savings.Where(a => a.User.Id == Id).OrderByDescending(b => b.DateCreated).Skip(skip).Take(10).ToList();

            return Json(savings);
        }
    }
}

