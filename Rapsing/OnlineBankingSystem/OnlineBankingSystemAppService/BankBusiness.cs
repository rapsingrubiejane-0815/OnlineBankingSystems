using System;
using OnlineBankingSystemModels;
using OnlineBankingSystemDataService;
namespace OnlineBankingSystemAppService
{
    public class BankBusiness
    {
        BankData data = new BankData();

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                data.account.Balance += amount;
                Console.WriteLine("Amount Deposited Successfully");
            }
            else
            {
                Console.WriteLine("Invalid Amount");
            }
        }

        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= data.account.Balance)
            {
                data.account.Balance -= amount;
                Console.WriteLine("Withdrawal Successful");
            }
            else
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine("Current Balance: Php " + data.account.Balance);
        }

        public void SendMoney(string recipient, double amount)
        {
            if (amount > 0 && amount <= data.account.Balance)
            {
                data.account.Balance -= amount;
                Console.WriteLine("Successfully sent Php " + amount + " to " + recipient);
            }
            else
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
            }
        }

        public void ReceiveMoney(string sender, double amount)
        {
            if (amount > 0)
            {
                data.account.Balance += amount;
                Console.WriteLine("Successfully received Php " + amount + " from " + sender);
            }
            else
            {
                Console.WriteLine("Invalid Amount");
            }
        }
    }


}
