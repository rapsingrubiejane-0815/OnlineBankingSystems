using OnlineBankingSystemModels;
using System.Security.Principal;
namespace OnlineBankingSystemDataService
{
    public class InMemoryData
    {
        public List<BankAccount> accounts = new List<BankAccount>(); 
        public void Add(BankAccount Account)
        {
            accounts.Add(Account);
        }
        public void CreateAccount(BankAccount account)
        {
            accounts.Add(account);
        }
        public BankAccount? GetBalance(double balance)
        {
            return accounts.FirstOrDefault(a => a.Balance== balance);
        }
        public BankAccount? GetById(Guid id)
        {
            return accounts.FirstOrDefault(a => a.AccountId == id);
        }

        public BankAccount? GetByUsername(string username)
        {
            return accounts.FirstOrDefault(a => a.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return accounts.Any(a => a.Username == username);
        }
        public List<BankAccount> GetAccounts()
        {
            return accounts;
        }

    }

}
