using System;
using OnlineBankingSystemAppService;

public class Program
{
    static BankBusiness bank = new BankBusiness();

    public static void Main()
    {
        int choice;

        Console.WriteLine("-----ONLINE BANKING SYSTEM-----\n");

        do
        {
            DisplayMenu();
            Console.Write("Enter a number from 1-6: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter amount to Deposit: ");
                    double deposit = Convert.ToDouble(Console.ReadLine());
                    bank.Deposit(deposit);
                    break;

                case 2:
                    Console.Write("Enter amount to Withdraw: ");
                    double withdraw = Convert.ToDouble(Console.ReadLine());
                    bank.Withdraw(withdraw);
                    break;

                case 3:
                    bank.CheckBalance();
                    break;

                case 4:
                    Console.Write("Enter recipient name: ");
                    string recipient = Console.ReadLine();
                    Console.Write("Enter amount to Send: ");
                    double send = Convert.ToDouble(Console.ReadLine());
                    bank.SendMoney(recipient, send);
                    break;

                case 5:
                    Console.Write("Enter sender name: ");
                    string sender = Console.ReadLine();
                    Console.Write("Enter amount received: ");
                    double receive = Convert.ToDouble(Console.ReadLine());
                    bank.ReceiveMoney(sender, receive);
                    break;

                case 6:
                    Console.WriteLine("Thank you for Banking with us");
                    break;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }

        } while (choice != 6);
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("\n1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Check Balance");
        Console.WriteLine("4. Send Money");
        Console.WriteLine("5. Receive Money");
        Console.WriteLine("6. Exit");
    }
}