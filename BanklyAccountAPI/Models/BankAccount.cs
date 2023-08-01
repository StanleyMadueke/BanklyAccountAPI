namespace BanklyAccountAPI.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }

        public string AccountHolderName { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
