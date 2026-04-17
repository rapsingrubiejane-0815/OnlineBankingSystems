using OnlineBankingSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystemDataService
{
    public interface IOnlineBankingDataService
    {
        void Add(BankAccount account);
        BankAccount? GetById(Guid id);
        BankAccount? GetByUsername(string username);
        void UpdateBalance(BankAccount account);
        BankAccount? GetBalances(double bal);
        List<BankAccount> GetBalance();

        
    }
}
