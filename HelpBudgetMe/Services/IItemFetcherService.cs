using HelpBudgetMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public interface IItemFetcherService
    {
        public User GetUser();
        public List<Need> GetNeeds(int amountToGet);
        public Task<Need> GetSpecificNeedAsync(int Id);
        public List<Need> GetMoreNeeds(int start);
        public List<Paycheck> GetPaychecks(int amountToGet);
        public Task<Paycheck> GetSpecificPaycheckAsync(int Id);
        public List<Paycheck> GetMorePaychecks(int start);
        public List<Want> GetWants(int amountToGet);
        public Task<Want> GetSpecificWantAsync(int Id);
        public List<Want> GetMoreWants(int start);
        public List<Saving> GetSavings(int amountToGet);
        public Task<Saving> GetSpecificSavingAsync(int Id);
        public List<Saving> GetMoreSavings(int start);
    }
}
