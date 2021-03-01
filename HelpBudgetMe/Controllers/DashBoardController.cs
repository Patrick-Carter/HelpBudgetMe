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
    public class DashBoardController : Controller
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public DashBoardController(ApplicationDBContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
       
        public IActionResult Index(DashboardViewModel model)
        {

            string Id = _userManager.GetUserId(User);

            User currentUser = _db.Users.Where(a => a.Id == Id).FirstOrDefault();



            model.CurrentMoney = currentUser.CurrentMoney;

            
            /*model.Needs = currentUser.Needs;
            model.NeedsBudget = currentUser.BudgetedForNeeds;
            model.Wants = currentUser.Wants;
            model.WantsBudget = currentUser.BudgetedForWants;
            model.Savings = currentUser.Savings;
            model.SavingsBudget = currentUser.BudgetedForSavings;
            model.CurrentMoney = currentUser.CurrentMoney;
            model.AllTimeEarned = currentUser.AllTimeEarned;
            model.AllTimeSpent = currentUser.AllTimeSpent;*/

            return  View(model);
        }
    }
}
