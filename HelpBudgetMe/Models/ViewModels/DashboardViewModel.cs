using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Need> Needs { get; set; }

        public decimal NeedsBudget { get; set; }

        public List<Want> Wants { get; set; }

        public decimal WantsBudget { get; set; }

        public List<Saving> Savings { get; set; }

        public decimal SavingsBudget { get; set; }

        public List<Paycheck> Paychecks { get; set; }

        public decimal CurrentMoney { get; set; }

        public decimal AllTimeEarned { get; set; }

        public decimal AllTimeSpent { get; set; }
    }
}
