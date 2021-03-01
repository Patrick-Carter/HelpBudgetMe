namespace HelpBudgetMe.Models
{
    public interface IBudgetType
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string Name { get; set; }
       
        public decimal Amount { get; set; }
      
        public System.DateTime DateCreated { get; set; }

    }
}