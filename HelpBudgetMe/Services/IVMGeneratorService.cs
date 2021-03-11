using HelpBudgetMe.Models;
using HelpBudgetMe.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Services
{
    public interface IVMGeneratorService
    {
        public NeedsViewModel CreateNeededModelForNeedIndex();
        public Task<EditViewModel> CreateNeededModelForEditNeedAsync(int Id);
        public Task<Need> CreateNeededModelForDeleteNeedAsync(int Id);
        public TransferViewModel CreateNeededModelForTransfer();
        public PaychecksViewModel CreateModelForPaycheckIndex();
        public Task<EditViewModel> CreateNeededModelForEditPaycheckAsync(int Id);
        public Task<Paycheck> CreateNeededModelForDeletePaycheckAsync(int Id);
        public WantsViewModel CreateNeededModelForWantsIndex();
        public Task<EditViewModel> CreateNeededModelForEditWants(int Id);
        public Task<Want> CreateNeededModelForDeleteWant(int Id);
    }
}
