using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Need> Needs { get; set; }

        public decimal NeedBudget { get; set; }

        public List<Want> Wants { get; set; }

        public decimal WantBudget { get; set; }

        public List<Saving> Savings { get; set; }

        public decimal SavingsBudget { get; set; }

        public decimal CurrentTotalMoney { get; set; }

        public decimal AllTimeTotalMoney { get; set; }

        public decimal AllTimeTotalSpent { get; set; }
    }
}
