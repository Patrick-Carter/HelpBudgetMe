using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class PaychecksViewModel
    {
        public List<Paycheck> Paychecks { get; set; }

        public decimal AllTimeEarned { get; set; }
    }
}
