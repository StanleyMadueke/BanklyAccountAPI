using BanklyAccountAPI.Models;

namespace BanklyAccountAPI.Repository
{
    public class BankAccountService : IBankAccountService
    {
        private readonly List<BankAccount> _accounts = new List<BankAccount>();
        private int _nextId = 1;

        public async Task<IEnumerable<BankAccount>> GetAllAccounts()
        {
            return _accounts;
        }

        public async Task<BankAccount> GetAccountById(int id)
        {
            return _accounts.FirstOrDefault(account => account.Id == id);
        }

        public async Task<BankAccount> AddAccount(BankAccount account)
        {
            account.Id = _nextId++;
            _accounts.Add(account);
            return account;
        }

        public async Task<IEnumerable<BankAccount>> UpdateAccount(BankAccount updatedAccount)
        {
            var index = _accounts.FindIndex(account => account.Id == updatedAccount.Id);
            if (index != -1)
            {
                _accounts[index] = updatedAccount;
            }
            return _accounts.ToList();
        }

        public async Task<IEnumerable<BankAccount>> DeleteAccount(int id)
        {
            _accounts.RemoveAll(account => account.Id == id);
            return _accounts.ToList();
        }

    }
}
