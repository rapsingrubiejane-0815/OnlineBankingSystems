using OnlineBankingSystemAppService;
using OnlineBankingSystemDataService;
using OnlineBankingSystemModels;
using System;

class Program
{
    static void Main(string[] args)
    {
        //var dataService = new BankingDataService();
        //var business = new BankBusiness(dataService);
        BankBusiness bb = new BankBusiness();

        BankAccount loggedInUser = null;

        while (loggedInUser == null)
        {
            Console.WriteLine("\n1. Create Account");
            Console.WriteLine("2. Login");
            Console.Write("Choose option: ");
            var choice = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    bb.Register(username, password, email);
                    Console.WriteLine("Account created! Please login.");
                }
                else if (choice == "2")
                {
                    loggedInUser = bb.Login(username, password);
                    Console.WriteLine($"Welcome, {loggedInUser.Username}!");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Send Money");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option:");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Amount: ");
                    double dep = double.Parse(Console.ReadLine());
                    bb.Deposit(loggedInUser.Username, dep);
                    break;

                case "2":
                    Console.Write("Amount to withdraw: ");
                    double wit = double.Parse(Console.ReadLine());
                    bb.Withdraw(loggedInUser.Username, wit);   
                    break;

                case "3":
                    bb.CheckBalance(loggedInUser.Username);     
                    break;

                case "4":
                    Console.Write("Enter receiver username: ");
                    string receiverUser = Console.ReadLine();
                    Console.Write("Enter amount: ");
                    double sendAmount = double.Parse(Console.ReadLine());
                    try { bb.SendMoney(loggedInUser.Username, receiverUser, sendAmount); }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;

                case "5":
                    running = false;
                    break;
            }
        }
    }
}