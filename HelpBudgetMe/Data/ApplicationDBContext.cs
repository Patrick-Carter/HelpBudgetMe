using HelpBudgetMe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpBudgetMe.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Need> Needs { get; set; }

        public DbSet<Want> Wants { get; set; }

        public DbSet<Saving> Savings { get; set; }

        public DbSet<Paycheck> Paychecks { get; set; }

    }
}
