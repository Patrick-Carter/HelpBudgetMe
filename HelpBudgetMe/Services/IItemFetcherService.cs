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
    }
}
