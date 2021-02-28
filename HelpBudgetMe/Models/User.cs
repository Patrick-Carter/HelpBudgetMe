using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models
{
    public class User : IdentityUser
    {
        public static object Identity { get; internal set; }
        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        [DataType(DataType.Currency)]
        public decimal CurrentMoney { get; set; }
        public List<Need> Needs { get; set; }
        public List<Want> Wants { get; set; }
        public List<Saving> Savings { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Paycheck> Paychecks { get; set; }

    }
}
