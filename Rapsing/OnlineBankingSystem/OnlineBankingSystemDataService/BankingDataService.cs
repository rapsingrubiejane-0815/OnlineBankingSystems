using OnlineBankingSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingSystemDataService
{
    public class BankingDataService
    {
      IOnlineBankingDataService _dataService;
      public BankingDataService(IOnlineBankingDataService onlineBankingDataService)
        {
            _dataService = onlineBankingDataService;    
        }
        public void Add(BankAccount account)
        {
            _dataService.Add(account);
        }

        public BankAccount? GetBalances(double bal) 
        {
            return _dataService.GetBalances(bal);
        }

        public List<BankAccount> GetBalance()
        {
            return _dataService.GetBalance();
        }
        public void UpdateBalance(BankAccount account)
        {
            _dataService.UpdateBalance(account);
        }
        
        public BankAccount? GetById(Guid id)
        {
            return _dataService.GetById(id);
        }


        public BankAccount? GetByUsername(string username)
        {
            return _dataService.GetByUsername(username);
        }
    }
}
