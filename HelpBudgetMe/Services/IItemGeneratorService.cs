using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public interface IItemGeneratorService
    {
        public Task CreateNeedAndPushToDbAsync(AddViewModel model);
        public Task EditNeedAndPushToDbAsync(EditViewModel model);
        public Task DeleteNeedAndPushToDbAsync(Need model);
        public Task CreatePaycheckAndPushToDbAsync(AddViewModel model);
        public Task EditPaycheckAndPushToDbAsync(EditViewModel model);
        public Task DeletePaycheckAndPushToDbAsync(Paycheck model);
        public Task CreateWantAndPushToDbAsync(AddViewModel model);
        public Task EditWantAndPushToDbAsync(EditViewModel model);
        public Task DeleteWantAndPushToDbAsync(Want model);
        public Task CreateSavingAndPushToDbAsync(AddViewModel model);
        public Task EditSavingAndPushToDbAsync(EditViewModel model);
        public Task DeleteSavingAndPushToDbAsync(Saving model);

    }
}
