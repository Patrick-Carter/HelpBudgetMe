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
            User user;
            
            try {
                string Id = _userManager.GetUserId(User);
                user = await _db.Users
                .Include(a => a.Needs).Take(10)
                .Include(a => a.Wants).Take(10)
                .Include(a => a.Savings).Take(10)
                .Include(a => a.Paychecks).Take(10)
                .Where(a => a.Id == Id)
                .FirstOrDefaultAsync();
            }
            catch
            {
                return BadRequest();
            }

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

            return  View(model);
        }
    }
}
