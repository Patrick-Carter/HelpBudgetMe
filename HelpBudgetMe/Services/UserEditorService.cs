using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public class UserEditorService : IUserEditorService
    {
        private readonly ApplicationDBContext _db;
        private readonly IItemFetcherService _itemFetcherService;
        private readonly UserManager<User> _userManager;
        public UserEditorService(ApplicationDBContext db,
            IItemFetcherService itemFetcherService,
            UserManager<User> userManager)
        {
            _db = db;
            _itemFetcherService = itemFetcherService;
            _userManager = userManager;
        }

        public async Task AddBudgetedForNeeds(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.BudgetedForNeeds += amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task AddBudgetedForSavings(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.BudgetedForSavings += amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task AddBudgetedForWants(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.BudgetedForWants += amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task AddCurrentMoney(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.CurrentMoney += amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task AddToAllTimeEarned(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.AllTimeEarned += amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task AddToAllTimeSpent(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.AllTimeSpent += amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task SubtractBudgetedForNeeds(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.BudgetedForNeeds -= amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task SubtractBudgetedForSavings(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.BudgetedForSavings -= amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task SubtractBudgetedForWants(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.BudgetedForWants -= amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task SubtractCurrentMoney(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.CurrentMoney -= amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task SubtractFromAllTimeEarned(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.AllTimeEarned -= amount;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task SubtractFromAllTimeSpent(decimal amount)
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            currentUser.AllTimeSpent -= amount;

            await _userManager.UpdateAsync(currentUser);
        }
    }
}
