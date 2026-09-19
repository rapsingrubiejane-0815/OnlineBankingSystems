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

        EmailService emailService = new EmailService();

        public void Register(string username, string password, string email)
        {
            var existing = onlineB.GetByUsername(username);

            if (existing != null)
                throw new Exception("Username already exists!");

          var newacc =  new BankAccount
            {

              AccountId = Guid.NewGuid(),
              Username = username,
                Password = password,
              Email = email,
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


        public void Deposit(string username, double amount)  
        {
            if (amount <= 0) { Console.WriteLine("Invalid Amount"); return; }

            var account = onlineB.GetByUsername(username);   
            if (account == null) { Console.WriteLine("Account not found"); return; }

            account.Balance += amount;
            onlineB.UpdateBalance(account);
            jbank.UpdateBalance(account);

            Console.WriteLine("Amount Deposited Successfully");
            Console.WriteLine("New Balance: Php " + account.Balance);

            try { emailService.SendEmail(account.Username, account.Email, "Deposit", amount, account.Balance); }
            catch (Exception ex) { Console.WriteLine("Email not sent: " + ex.Message); }
        }
        public void Withdraw(string username, double amount)  
        {
            var account = onlineB.GetByUsername(username);
            if (account == null || amount <= 0 || amount > account.Balance)
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
                return;
            }

            account.Balance -= amount;
            onlineB.UpdateBalance(account);
            jbank.UpdateBalance(account);

            Console.WriteLine("Withdrawal Successful");

            try { emailService.SendEmail(account.Username, account.Email, "Withdrawal", amount, account.Balance); }
            catch (Exception ex) { Console.WriteLine("Email not sent: " + ex.Message); }
        }

        public void CheckBalance(string username)  
        {
            var acc = onlineB.GetByUsername(username);
            if (acc != null) Console.WriteLine("Current Balance: Php " + acc.Balance);
        }


        public void SendMoney(string senderUsername, string recipient, double amount)
        {
            var sender = onlineB.GetByUsername(senderUsername);
            var receiver = onlineB.GetByUsername(recipient);

            if (sender == null) { Console.WriteLine("Sender not found"); return; }
            if (receiver == null) { Console.WriteLine("Receiver not found"); return; }
            if (amount <= 0 || amount > sender.Balance)
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
                return;
            }

            sender.Balance -= amount;
            receiver.Balance += amount;

            onlineB.UpdateBalance(sender);
            onlineB.UpdateBalance(receiver);
            jbank.UpdateBalance(sender);
            jbank.UpdateBalance(receiver);

            Console.WriteLine($"Successfully sent Php {amount} to {recipient}");

            // Notify both parties
            try { emailService.SendEmail(sender.Username, sender.Email, "Money Transfer (Sent)", amount, sender.Balance); }
            catch (Exception ex) { Console.WriteLine("Email not sent to sender: " + ex.Message); }

            try { emailService.SendEmail(receiver.Username, receiver.Email, "Money Received", amount, receiver.Balance); }
            catch (Exception ex) { Console.WriteLine("Email not sent to receiver: " + ex.Message); }
        }
    }
}