using HelpBudgetMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public interface IItemFetcherService
    {
        public Task<User> GetUserAsync();
        public Task<List<Need>> GetNeeds(int amountToGet);
        public Task<Need> GetSpecificNeedAsync(int Id);
        public Task<List<Need>> GetMoreNeedsAsync(int start);
        public Task<List<Paycheck>> GetPaychecks(int amountToGet);
        public Task<Paycheck> GetSpecificPaycheckAsync(int Id);
        public Task<List<Paycheck>> GetMorePaychecksAsync(int start);
    }
}
