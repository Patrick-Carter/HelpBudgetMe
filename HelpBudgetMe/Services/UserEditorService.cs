using HelpBudgetMe.Data;
using HelpBudgetMe.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public class UserEditorService : IUserEditorService
    {
        private readonly ApplicationDBContext _db;
        private readonly IItemFetcherService _itemFetcherService;
        private readonly UserManager<User> _userManager;
        private readonly User _currentUser;
        public UserEditorService(ApplicationDBContext db,
            IItemFetcherService itemFetcherService,
            UserManager<User> userManager)
        {
            _db = db;
            _itemFetcherService = itemFetcherService;
            _userManager = userManager;
            _currentUser = itemFetcherService.GetUser();
        }

        public async Task AddBudgetedForNeeds(decimal amount)
        {
            _currentUser.BudgetedForNeeds += amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task AddBudgetedForSavings(decimal amount)
        {
            _currentUser.BudgetedForSavings += amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task AddBudgetedForWants(decimal amount)
        {
            _currentUser.BudgetedForWants += amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task AddCurrentMoney(decimal amount)
        {
            _currentUser.CurrentMoney += amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task AddToAllTimeEarned(decimal amount)
        {
            _currentUser.AllTimeEarned += amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task AddToAllTimeSpent(decimal amount)
        {
            _currentUser.AllTimeSpent += amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task SubtractBudgetedForNeeds(decimal amount)
        {
            _currentUser.BudgetedForNeeds -= amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task SubtractBudgetedForSavings(decimal amount)
        {
            _currentUser.BudgetedForSavings -= amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task SubtractBudgetedForWants(decimal amount)
        {
            _currentUser.BudgetedForWants -= amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task SubtractCurrentMoney(decimal amount)
        {
            _currentUser.CurrentMoney -= amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task SubtractFromAllTimeEarned(decimal amount)
        {
            _currentUser.AllTimeEarned -= amount;

            await _userManager.UpdateAsync(_currentUser);
        }

        public async Task SubtractFromAllTimeSpent(decimal amount)
        {
            _currentUser.AllTimeSpent -= amount;

            await _userManager.UpdateAsync(_currentUser);
        }
    }
}