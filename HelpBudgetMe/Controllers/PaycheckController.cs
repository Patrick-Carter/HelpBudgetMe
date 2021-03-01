using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Controllers
{
    public class PaycheckController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;

        public PaycheckController(ApplicationDBContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddPaycheck(Paycheck model)
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
    }
}
