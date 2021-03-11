using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public interface IUserEditorService
    {
        public Task AddCurrentMoney(decimal amount);
        public Task SubtractCurrentMoney(decimal amount);
        public Task AddBudgetedForNeeds(decimal amount);
        public Task SubtractBudgetedForNeeds(decimal amount);
        public Task AddToAllTimeSpent(decimal amount);
        public Task SubtractFromAllTimeSpent(decimal amount);
        public Task AddBudgetedForWants(decimal amount);
        public Task AddBudgetedForSavings(decimal amount);
    }
}
