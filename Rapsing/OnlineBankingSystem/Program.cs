using System;

public class HelloWorld
{
    static double balance = 1000; 

    public static void Main(string[] args)
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
                    Deposit();
                    break;

                case 2:
                    Withdraw();
                    break;

                case 3:
                    CheckBalance();
                    break;

                case 4:
                    SendMoney();
                    break;

                case 5:
                    ReceiveMoney();
                    break;

                case 6:
                    Console.WriteLine("Thank you for Banking with us");
                    break;

                default:
                    Console.WriteLine("Invalid Choice\n");
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

  
    public static void Deposit()
    {
        Console.Write("Enter amount to Deposit: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine("Amount Deposited Successfully");
        }
        else
        {
            Console.WriteLine("Invalid Amount");
        }
    }

  
    public static void Withdraw()
    {
        Console.Write("Enter amount to Withdraw: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine("Withdrawal Successfully");
        }
        else
        {
            Console.WriteLine("Insufficient Balance or Invalid Amount");
        }
    }

 
    public static void CheckBalance()
    {
        Console.WriteLine("Current Balance: Php " + balance);
    }

    
    public static void SendMoney()
    {
        Console.Write("Enter recipient name: ");
        string recipient = Console.ReadLine();

        Console.Write("Enter amount to Send: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine("Successfully sent Php " + amount + " to " + recipient);
        }
        else
        {
            Console.WriteLine("Insufficient Balance or Invalid Amount");
        }
    }

                    
    public static void ReceiveMoney()
    {
        Console.Write("Enter sender name: ");
        string sender = Console.ReadLine();

        Console.Write("Enter amount received: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine("Successfully received Php " + amount + " from " + sender);
        }
        else
        {
            Console.WriteLine("Invalid Amount");
            Console.WriteLine("rapsing");
        }
    }
}