using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels.PaycheckVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    [Authorize]
    public class PaycheckController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public PaycheckController(ApplicationDBContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult AddPaycheck()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditPaycheck(int Id)
        {
            Paycheck paycheck = _db.Paychecks.Where(a => a.Id == Id).FirstOrDefault();

            var model = new EditPaycheckViewModel()
            {
                Id = paycheck.Id,
                Name = paycheck.Name,
                Amount = paycheck.Amount,
                PreviousAmount = paycheck.Amount,
                DateCreated = paycheck.DateCreated
            };

            return View(model);
        }

        public IActionResult DeletePaycheck(int Id)
        {

            Paycheck paycheck = _db.Paychecks.Where(a => a.Id == Id).FirstOrDefault();

            return View(paycheck);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeletePaycheck(Paycheck paycheck)
        {
            if (ModelState.IsValid)
            {
                try { 
                    string UserId = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == UserId).FirstOrDefault();

                    currentUser.CurrentMoney -= paycheck.Amount;
                    currentUser.BudgetedForNeeds = currentUser.CurrentMoney * .5m;
                    currentUser.BudgetedForWants = currentUser.CurrentMoney * .3m;
                    currentUser.BudgetedForSavings = currentUser.CurrentMoney * .2m;
                    currentUser.AllTimeEarned -= paycheck.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    _db.Paychecks.Remove(paycheck);
                    _db.SaveChanges();


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
        public async Task<IActionResult> AddPaycheck(Paycheck model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();


                    var paycheck = new Paycheck
                    {
                        Name = model.Name,
                        Amount = model.Amount,
                        User = currentUser,
                        DateCreated = DateTime.Now
                    };


                    currentUser.CurrentMoney += model.Amount;
                    currentUser.AllTimeEarned += model.Amount;
                    currentUser.BudgetedForNeeds += (model.Amount * .5m);
                    currentUser.BudgetedForWants += (model.Amount * .3m);
                    currentUser.BudgetedForSavings += (model.Amount * .2m);
                    _db.Add(paycheck);
                    await _userManager.UpdateAsync(currentUser);
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
        public async Task<IActionResult> EditPaycheck(EditPaycheckViewModel model)
        {
            if (ModelState.IsValid) { 
                try { 
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();

                    var paycheck = new Paycheck()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Amount = model.Amount,
                        DateCreated = model.DateCreated,
                        User = currentUser
                    };

                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    currentUser.CurrentMoney -= amountChanged;
                    currentUser.BudgetedForNeeds = currentUser.CurrentMoney * .5m;
                    currentUser.BudgetedForWants = currentUser.CurrentMoney * .3m;
                    currentUser.BudgetedForSavings = currentUser.CurrentMoney * .2m;
                    currentUser.AllTimeEarned -= amountChanged;

                    await _userManager.UpdateAsync(currentUser);
                    _db.Update(paycheck);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Dashboard");
                }
                catch { return BadRequest(); }
            }
            else
            {
                return View(model);
            }

            
        }
    }
}
