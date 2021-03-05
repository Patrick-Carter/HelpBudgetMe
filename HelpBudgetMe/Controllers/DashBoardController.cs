using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public DashBoardController(ApplicationDBContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
       
        public async Task<IActionResult> Index()
        {
            
            
            try {
                string Id = _userManager.GetUserId(User);

                var user = await _db.Users.Where(a => a.Id == Id).Select(b => new User
                {
                    Paychecks = b.Paychecks.OrderByDescending(a => a.DateCreated).Take(5).ToList(),
                    Needs = b.Needs.OrderByDescending(a => a.DateCreated).Take(5).ToList(),
                    Wants = b.Wants.OrderByDescending(a => a.DateCreated).Take(5).ToList(),
                    Savings = b.Savings.OrderByDescending(a => a.DateCreated).Take(5).ToList(),
                    BudgetedForNeeds = b.BudgetedForNeeds,
                    BudgetedForWants = b.BudgetedForWants,
                    BudgetedForSavings = b.BudgetedForSavings,
                    AllTimeEarned = b.AllTimeEarned,
                    AllTimeSpent = b.AllTimeSpent,
                    CurrentMoney = b.CurrentMoney
                }).FirstOrDefaultAsync();

                DashboardViewModel model = new DashboardViewModel();

                model.Needs = user.Needs;
                model.Wants = user.Wants;
                model.Savings = user.Savings;
                model.Paychecks = user.Paychecks;
                model.NeedsBudget = user.BudgetedForNeeds;
                model.WantsBudget = user.BudgetedForWants;
                model.SavingsBudget = user.BudgetedForSavings;
                model.AllTimeEarned = user.AllTimeEarned;
                model.AllTimeSpent = user.AllTimeSpent;
                model.CurrentMoney = user.CurrentMoney;

                return View(model);

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
