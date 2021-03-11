using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public class ItemFetcherService : IItemFetcherService
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly User _user;

        public ItemFetcherService(ApplicationDBContext db,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _user = _userManager.GetUserAsync(httpContextAccessor.HttpContext.User)
                .GetAwaiter()
                .GetResult();
        }
        public List<Need> GetNeeds(int amountToGet)
        {
            List<Need> needs = _db.Needs.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Take(amountToGet).ToList();
            return needs;
        }

        public  User GetUser()
        {
            return _user;
        }

        public async Task<Need> GetSpecificNeedAsync(int Id)
        {
            Need need = await _db.Needs.Where(a => (a.Id == Id) && (a.User == _user)).FirstOrDefaultAsync();
            return need;
        }

        public List<Need> GetMoreNeeds(int start)
        {
            var needs = _db.Needs.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Skip(start).Take(10).ToList();
            return needs;
        }

        public List<Paycheck> GetPaychecks(int amountToGet)
        {
            List<Paycheck> paychecks = _db.Paychecks.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Take(amountToGet).ToList();
            return paychecks;
        }

        public async Task<Paycheck> GetSpecificPaycheckAsync(int Id)
        {
            Paycheck paycheck = await _db.Paychecks.Where(a => (a.Id == Id) && (a.User == _user)).FirstOrDefaultAsync();
            return paycheck;
        }

        public List<Paycheck> GetMorePaychecks(int start)
        {
            var paychecks = _db.Paychecks.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Skip(start).Take(10).ToList();
            return paychecks;
        }

        public List<Want> GetWants(int amountToGet)
        {
            List<Want> wants = _db.Wants.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Take(amountToGet).ToList();
            return wants;
        }

        public async Task<Want> GetSpecificWantAsync(int Id)
        {
            Want want = await _db.Wants.Where(a => (a.Id == Id) && (a.User == _user)).FirstOrDefaultAsync();
            return want;
        }

        public List<Want> GetMoreWants(int start)
        {
            var wants = _db.Wants.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Skip(start).Take(10).ToList();
            return wants;
        }

        public List<Saving> GetSavings(int amountToGet)
        {
            List<Saving> savings = _db.Savings.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Take(amountToGet).ToList();
            return savings;
        }

        public async Task<Saving> GetSpecificSavingAsync(int Id)
        {
            Saving saving = await _db.Savings.Where(a => (a.Id == Id) && (a.User == _user)).FirstOrDefaultAsync();
            return saving;
        }

        public List<Saving> GetMoreSavings(int start)
        {
            var savings = _db.Savings.Where(a => a.User == _user).OrderByDescending(b => b.DateCreated).Skip(start).Take(10).ToList();
            return savings;
        }
    }
}
