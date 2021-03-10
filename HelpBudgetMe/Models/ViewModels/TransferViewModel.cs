using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class TransferViewModel
    {
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        public decimal BudgetedForNeeds { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        public decimal BudgetedForWants { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        public decimal BudgetedForSavings { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "amount must be positive")]
        public decimal TransferToNeeds { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "amount must be positive")]
        public decimal TransferToWants { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        [Range(0, double.MaxValue, ErrorMessage = "amount must be positive")]
        public decimal TransferToSavings { get; set; }
    }
}
