using OnlineBankingSystem;
using OnlineBankingSystemDataService;
using OnlineBankingSystemModels;
using System;
using System.Security.Principal;

namespace OnlineBankingSystemAppService
{
    public class BankBusiness
    {
        InMemoryData inn = new InMemoryData();
        BankingDataService onlineB = new BankingDataService(new OnlineBankingDBData());

        OnlineBankingJson jbank = new OnlineBankingJson();

        public void Register(string username, string password)
        {
            var existing = onlineB.GetByUsername(username);

            if (existing != null)
                throw new Exception("Username already exists!");

          var newacc =  new BankAccount
            {

              AccountId = Guid.NewGuid(),
              Username = username,
                Password = password,
                Balance = 0
            };
            onlineB.Add(newacc);
            jbank.Add(newacc);

        }

        public BankAccount Login(string username, string password)
        {
            var account = onlineB.GetByUsername(username);

            if (account == null || account.Password != password)
                throw new Exception("Invalid username or password!");

            return account;
        }

        public bool Authenticate(string username, string password)
        {
            var account = onlineB.GetByUsername(username);

            if (account == null)
                return false;

            return account.Password == password;
        }
     
        public BankAccount? GetAccount(Guid accountId)
        {
            return onlineB.GetById(accountId);
        }


        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                var accounts = onlineB.GetBalance();
              //  var acc =  jbank.GetBalance();
             //   var accs = acc.FirstOrDefault();
                var account = accounts.FirstOrDefault();

                if (account != null)
                {
                    account.Balance += amount;

                    onlineB.UpdateBalance(account);
                    jbank.UpdateBalance(account);

                    Console.WriteLine("Amount Deposited Successfully");
                    Console.WriteLine("New Balance: Php " + account.Balance);
                }
            }
            else
            {
                Console.WriteLine("Invalid Amount");
            }

        }
        public void Withdraw(double amount)
        {
            var accounts = onlineB.GetBalance();
         //   var acc = inn.GetBalance();
            var account = accounts.FirstOrDefault();

            if (account != null && amount > 0 && amount <= account.Balance)
            {
                account.Balance -= amount;

                     onlineB.UpdateBalance(account);
                     jbank.UpdateBalance(account);
              

                Console.WriteLine("Withdrawal Successful");
            }
            else
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
            }
        }

        public void CheckBalance()
        {
            var balances = onlineB.GetBalance();
            var Obalance = jbank.GetBalance();


            foreach (var acc in balances)
            {
                Console.WriteLine("Current Balance: Php " + acc.Balance);
            }
        }

       
        public void SendMoney(string recipient, double amount)
        {
            var accounts = onlineB.GetBalance();
            var acc = jbank.GetBalance();
            var account = accounts.FirstOrDefault();

            if (account != null && amount > 0 && amount <= account.Balance)
            {
                account.Balance -= amount;

                onlineB.UpdateBalance(account);
                jbank.UpdateBalance(account);

                Console.WriteLine("Successfully sent Php " + amount + " to " + recipient);
            }
            else
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
            }
        }
    }
}