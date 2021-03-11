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
    }
}
