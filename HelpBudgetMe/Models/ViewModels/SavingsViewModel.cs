using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class SavingsViewModel
    {
        public List<Saving> Savings { get; set; }

        public decimal BudgetedForSavings { get; set; }
    }
}
