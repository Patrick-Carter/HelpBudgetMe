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
    public class WantController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public WantController(ApplicationDBContext db, UserManager<User> userManager)
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

                var wants = _db.Wants.Where(a => a.User == currentUser).OrderByDescending(b => b.DateCreated).Take(10).ToList();

                WantsViewModel model = new WantsViewModel()
                {
                    Wants = wants,
                    BudgetedForWants = currentUser.BudgetedForWants
                };

                return View(model);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult AddWant()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditWant(int Id)
        {
            Want want  = _db.Wants.Where(a => a.Id == Id).FirstOrDefault();

            var model = new EditViewModel()
            {
                Id = want.Id,
                Name = want.Name,
                Amount = want.Amount,
                PreviousAmount = want.Amount,
                DateCreated = want.DateCreated
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteWant(int Id)
        {
            Want want = _db.Wants.Where(a => a.Id == Id).FirstOrDefault();

            return View(want);
        }

        [HttpGet]
        public async Task<IActionResult> TransferFromWants()
        {
            string Id = _userManager.GetUserId(User);
            User currentUser = await _db.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();

            var model = new TransferViewModel()
            {
                BudgetedForNeeds = currentUser.BudgetedForNeeds,
                BudgetedForWants = currentUser.BudgetedForWants,
                BudgetedForSavings = currentUser.BudgetedForSavings,
                TransferToNeeds = 0m,
                TransferToWants = 0m,
                TransferToSavings = 0m
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWant(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();

                    Want want = new Want()
                    {
                        Name = model.Name,
                        Amount = model.Amount,
                        User = currentUser,
                        DateCreated = DateTime.Now
                    };

                    currentUser.CurrentMoney -= model.Amount;
                    currentUser.BudgetedForWants -= model.Amount;
                    currentUser.AllTimeSpent += model.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    await _db.Wants.AddAsync(want);
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

        public async Task<IActionResult> EditWant(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();
                    Want want = _db.Wants.Where(a => a.Id == model.Id).FirstOrDefault();


                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    currentUser.CurrentMoney += amountChanged;
                    currentUser.AllTimeSpent -= amountChanged;
                    currentUser.BudgetedForWants += amountChanged;
                    await _userManager.UpdateAsync(currentUser);
                    want.Amount = model.Amount;
                    want.Name = model.Name;
                    _db.Wants.Update(want);
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

        public async Task<IActionResult> DeleteWant(Want model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string UserId = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == UserId).FirstOrDefault();
                    Want want = _db.Wants.Where(a => (a.Id == model.Id) && (a.User == currentUser)).FirstOrDefault();

                    currentUser.CurrentMoney += want.Amount;
                    currentUser.BudgetedForWants += want.Amount;
                    currentUser.AllTimeSpent -= want.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    _db.Wants.Remove(want);
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
        public async Task<IActionResult> TransferFromWants(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UserId = _userManager.GetUserId(User);
                User currentUser = _db.Users.Where(a => a.Id == UserId).FirstOrDefault();

                currentUser.BudgetedForWants -= (model.TransferToSavings + model.TransferToNeeds);
                currentUser.BudgetedForNeeds += model.TransferToNeeds;
                currentUser.BudgetedForSavings += model.TransferToSavings;
                await _userManager.UpdateAsync(currentUser);

                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        [HttpPost]
        [Route("api/GetMoreWant")]

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

            var wants = _db.Wants.Where(a => a.User.Id == Id).OrderByDescending(b => b.DateCreated).Skip(skip).Take(10).ToList();

            return Json(wants);
        }
    }
}
