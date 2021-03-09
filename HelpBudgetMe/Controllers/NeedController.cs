﻿using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class NeedController : Controller
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public NeedController(ApplicationDBContext db, UserManager<User> userManager)
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

                var needs = _db.Needs.Where(a => a.User == currentUser).OrderByDescending(b => b.DateCreated).Take(10).ToList();

                NeedsViewModel model = new NeedsViewModel()
                {
                    Needs = needs,
                    BudgetedForNeeds = currentUser.BudgetedForNeeds
                };

                return View(model);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult AddNeed()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditNeed(int Id)
        {
            Need need = _db.Needs.Where(a => a.Id == Id).FirstOrDefault();

            var model = new EditViewModel()
            {
                Id = need.Id,
                Name = need.Name,
                Amount = need.Amount,
                PreviousAmount = need.Amount,
                DateCreated = need.DateCreated
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteNeed(int Id)
        {
            Need need = _db.Needs.Where(a => a.Id == Id).FirstOrDefault();

            return View(need);
        }

        [HttpGet]
        public async Task<IActionResult> TransferFromNeeds()
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
        public async Task<IActionResult> AddNeed(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();

                    Need need = new Need()
                    {
                        Name = model.Name,
                        Amount = model.Amount,
                        User = currentUser,
                        DateCreated = DateTime.Now
                    };

                    currentUser.CurrentMoney -= model.Amount;
                    currentUser.BudgetedForNeeds -= model.Amount;
                    currentUser.AllTimeSpent += model.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    await _db.Needs.AddAsync(need);
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

        public async Task<IActionResult> EditNeed(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Id = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();
                    Need need = _db.Needs.Where(a => a.Id == model.Id).FirstOrDefault();
                    

                    decimal amountChanged = model.PreviousAmount - model.Amount;

                    currentUser.CurrentMoney += amountChanged;
                    currentUser.AllTimeSpent -= amountChanged;
                    currentUser.BudgetedForNeeds += amountChanged;
                    await _userManager.UpdateAsync(currentUser);
                    need.Amount = model.Amount;
                    need.Name = model.Name;
                    _db.Needs.Update(need);
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

        public async Task<IActionResult> DeleteNeed(Need need)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string UserId = _userManager.GetUserId(User);
                    User currentUser = _db.Users.Where(a => a.Id == UserId).FirstOrDefault();

                    currentUser.CurrentMoney += need.Amount;
                    currentUser.BudgetedForNeeds += need.Amount;
                    currentUser.AllTimeSpent -= need.Amount;
                    await _userManager.UpdateAsync(currentUser);
                    _db.Needs.Remove(need);
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
        public async Task<IActionResult> TransferFromNeeds(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UserId = _userManager.GetUserId(User);
                User currentUser = _db.Users.Where(a => a.Id == UserId).FirstOrDefault();

                currentUser.BudgetedForNeeds -= (model.TransferToSavings + model.TransferToWants);
                currentUser.BudgetedForWants += model.TransferToWants;
                currentUser.BudgetedForSavings += model.TransferToSavings;
                await _userManager.UpdateAsync(currentUser);

                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        [HttpPost]
        [Route("api/GetMoreNeed")]

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

            var needs = _db.Needs.Where(a => a.User.Id == Id).OrderByDescending(b => b.DateCreated).Skip(skip).Take(10).ToList();

            return Json(needs);
        }
    }
}
