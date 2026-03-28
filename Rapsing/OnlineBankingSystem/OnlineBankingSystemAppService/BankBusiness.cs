using OnlineBankingSystem;
using OnlineBankingSystemDataService;
using OnlineBankingSystemModels;
using System;

namespace OnlineBankingSystemAppService
{
    public class BankBusiness
    {
        BankingDataService onlineB = new BankingDataService(new OnlineBankingDBData());

        OnlineBankingJson jbank = new OnlineBankingJson();

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                var accounts = onlineB.GetBalance();
                var acc =  jbank.GetBalance();
                var accs = acc.FirstOrDefault();
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
            var acc = jbank.GetBalance();
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
        public void ReceiveMoney(string sender, double amount)
        {
            if (amount > 0)
            {
                var accounts = onlineB.GetBalance();
                var acc = jbank.GetBalance();
                var account = accounts.FirstOrDefault();

                if (account != null)
                {
                    account.Balance += amount;

                    onlineB.UpdateBalance(account);
                    jbank.UpdateBalance(account);

                    Console.WriteLine("Successfully received Php " + amount + " from " + sender);
                }
            }
            else
            {
                Console.WriteLine("Invalid Amount");
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