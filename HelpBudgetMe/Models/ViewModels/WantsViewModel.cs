using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class WantsViewModel
    {
        public List<Want> Wants { get; set; }
        public decimal BudgetedForWants { get; set; }
    }
}
