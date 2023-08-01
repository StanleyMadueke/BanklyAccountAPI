using BanklyAccountAPI.Models;

namespace BanklyAccountAPI.Repository
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccount>> GetAllAccounts();
        Task<BankAccount> GetAccountById(int id);
        Task<BankAccount> AddAccount(BankAccount account);
        Task<IEnumerable<BankAccount>> UpdateAccount(BankAccount updatedAccount);
        Task<IEnumerable<BankAccount>> DeleteAccount(int id);

    }

    
}
