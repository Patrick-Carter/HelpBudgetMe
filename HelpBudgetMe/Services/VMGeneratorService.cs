using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public class VMGeneratorService : IVMGeneratorService
    {
        private readonly IItemFetcherService _itemFetcherService;
        public VMGeneratorService(IItemFetcherService itemFetcherService)
        {
            _itemFetcherService = itemFetcherService;
        }
        public async Task<EditViewModel> CreateNeededModelForEditNeedAsync(int Id)
        {
            Need need = await _itemFetcherService.GetSpecificNeedAsync(Id);

            var model = new EditViewModel()
            {
                Id = need.Id,
                Name = need.Name,
                Amount = need.Amount,
                PreviousAmount = need.Amount,
                DateCreated = need.DateCreated
            };

            return model;
        }
        public async Task<NeedsViewModel> CreateNeededModelForNeedIndexAsync()
        {
            User currentUser = await _itemFetcherService.GetUserAsync();
            var needs = await _itemFetcherService.GetNeeds(10);

            NeedsViewModel model = new NeedsViewModel()
            {
                Needs = needs,
                BudgetedForNeeds = currentUser.BudgetedForNeeds
            };

            return model;
        }
        public async Task<TransferViewModel> CreateNeededModelForTransferAsync()
        {
            User currentUser = await _itemFetcherService.GetUserAsync();

            var model = new TransferViewModel()
            {
                BudgetedForNeeds = currentUser.BudgetedForNeeds,
                BudgetedForWants = currentUser.BudgetedForWants,
                BudgetedForSavings = currentUser.BudgetedForSavings,
                TransferToNeeds = 0m,
                TransferToWants = 0m,
                TransferToSavings = 0m
            };

            return model;
        }
        public async Task<Need> CreateNeededModelForDeleteNeedAsync(int Id)
        {
            Need need = await _itemFetcherService.GetSpecificNeedAsync(Id);
            return need;
        }
    }
}
