using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class NeedsViewModel
    {
        public List<Need> Needs { get; set; }

        public decimal BudgetedForNeeds { get; set; }
    }
}
