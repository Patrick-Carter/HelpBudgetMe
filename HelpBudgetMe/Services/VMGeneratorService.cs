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
        private readonly User _user;
        public VMGeneratorService(IItemFetcherService itemFetcherService)
        {
            _itemFetcherService = itemFetcherService;
            _user = itemFetcherService.GetUser();
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
        public NeedsViewModel CreateNeededModelForNeedIndex()
        {
            var needs =  _itemFetcherService.GetNeeds(10);

            NeedsViewModel model = new NeedsViewModel()
            {
                Needs = needs,
                BudgetedForNeeds = _user.BudgetedForNeeds
            };

            return model;
        }
        public TransferViewModel CreateNeededModelForTransfer()
        {
            var model = new TransferViewModel()
            {
                BudgetedForNeeds = _user.BudgetedForNeeds,
                BudgetedForWants = _user.BudgetedForWants,
                BudgetedForSavings = _user.BudgetedForSavings,
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

        public PaychecksViewModel CreateModelForPaycheckIndex()
        {
            var paychecks = _itemFetcherService.GetPaychecks(10);

            PaychecksViewModel model = new PaychecksViewModel()
            {
                Paychecks = paychecks,
                AllTimeEarned = _user.AllTimeEarned

            };

            return model;
        }

        public async Task<EditViewModel> CreateNeededModelForEditPaycheckAsync(int Id)
        {
            Paycheck paycheck = await _itemFetcherService.GetSpecificPaycheckAsync(Id);

            var model = new EditViewModel()
            {
                Id = paycheck.Id,
                Name = paycheck.Name,
                Amount = paycheck.Amount,
                PreviousAmount = paycheck.Amount,
                DateCreated = paycheck.DateCreated
            };

            return model;
        }

        public async Task<Paycheck> CreateNeededModelForDeletePaycheckAsync(int Id)
        {
            Paycheck paycheck = await _itemFetcherService.GetSpecificPaycheckAsync(Id);
            return paycheck;
        }

        public WantsViewModel CreateNeededModelForWantsIndex()
        {
            var wants = _itemFetcherService.GetWants(10);

            WantsViewModel model = new WantsViewModel()
            {
                Wants = wants,
                BudgetedForWants = _user.BudgetedForWants
            };

            return model;
        }

        public async Task<EditViewModel> CreateNeededModelForEditWants(int Id)
        {
            Want want = await _itemFetcherService.GetSpecificWantAsync(Id);

            var model = new EditViewModel()
            {
                Id = want.Id,
                Name = want.Name,
                Amount = want.Amount,
                PreviousAmount = want.Amount,
                DateCreated = want.DateCreated
            };

            return model;
        }

        public async Task<Want> CreateNeededModelForDeleteWantAsync(int Id)
        {
            Want want = await _itemFetcherService.GetSpecificWantAsync(Id);
            return want;
        }

        public SavingsViewModel CreateNeededModelForSavingIndex()
        {
            var savings = _itemFetcherService.GetSavings(10);

            SavingsViewModel model = new SavingsViewModel()
            {
                Savings = savings,
                BudgetedForSavings = _user.BudgetedForSavings
            };

            return model;
        }

        public async Task<EditViewModel> CreateNeededModelForEditSavingAsync(int Id)
        {
            Saving saving = await _itemFetcherService.GetSpecificSavingAsync(Id);

            var model = new EditViewModel()
            {
                Id = saving.Id,
                Name = saving.Name,
                Amount = saving.Amount,
                PreviousAmount = saving.Amount,
                DateCreated = saving.DateCreated
            };

            return model;
        }

        public async Task<Saving> CreateNeededModelForDeleteSavingAsync(int Id)
        {
            Saving saving = await _itemFetcherService.GetSpecificSavingAsync(Id);
            return saving;
        }
    }
}
