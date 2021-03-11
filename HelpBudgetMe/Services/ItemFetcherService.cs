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

        public ItemFetcherService(ApplicationDBContext db,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Need>> GetNeeds(int amountToGet)
        {
            User user = await GetUser();
            List<Need> needs = _db.Needs.Where(a => a.User.Id == user.Id).OrderByDescending(b => b.DateCreated).Take(amountToGet).ToList();

            return needs;
        }

        public async Task<User> GetUserAsync()
        {
            User user = await GetUser();
            return user;
        }

        public async Task<Need> GetSpecificNeedAsync(int Id)
        {
            User user = await GetUser();
            Need need = await _db.Needs.Where(a => (a.Id == Id) && (a.User == user)).FirstOrDefaultAsync();
            return need;
        }

        public async Task<List<Need>> GetMoreNeedsAsync(int start)
        {
            User user = await GetUser();
            var needs = _db.Needs.Where(a => a.User == user).OrderByDescending(b => b.DateCreated).Skip(start).Take(10).ToList();

            return needs;
        }

        private async Task<User> GetUser()
        {
            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user;
        }

        
    }
}
