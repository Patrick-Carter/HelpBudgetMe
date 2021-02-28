namespace HelpBudgetMe.Models
{
    public interface IBudgetType
    {
        public int Id { get; set; }

        public User User { get; set; }
        public Expense Expense { get; set; }            
  
    }
}