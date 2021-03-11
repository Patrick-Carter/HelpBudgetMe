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
        public Task<NeedsViewModel> CreateNeededModelForNeedIndexAsync();
        public Task<EditViewModel> CreateNeededModelForEditNeedAsync(int Id);
        public Task<Need> CreateNeededModelForDeleteNeedAsync(int Id);
        public Task<TransferViewModel> CreateNeededModelForTransferAsync();
        public Task<PaychecksViewModel> CreateModelForPaycheckIndexAsync();
        public Task<EditViewModel> CreateNeededModelForEditPaycheckAsync(int Id);
        public Task<Paycheck> CreateNeededModelForDeletePaycheckAsync(int Id);
    }
}
