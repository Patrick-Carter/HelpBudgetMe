﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models
{
    public class Want : IBudgetType
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "value must be positive")]
        public decimal Amount { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

    }
}
