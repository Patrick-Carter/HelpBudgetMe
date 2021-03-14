using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Models.ViewModels
{
    public class EditViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "amount must be positive")]
        public decimal Amount { get; set; }
        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [DataType(DataType.Currency)]
        public decimal PreviousAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}
