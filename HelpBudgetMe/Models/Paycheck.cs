﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models
{
    public class Paycheck
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public User User { get; set; }
    }
}