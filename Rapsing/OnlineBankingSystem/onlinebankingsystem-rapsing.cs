namespace OnlineBankingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double balance, deposit, withdraw;
            int choice;
           
            do
            {
                Console.WriteLine("\n===== SIMPLE BANKING SYSTEM =====");
                Console.WriteLine("1 - Deposit");
                Console.WriteLine("2 - Withdraw");
                Console.WriteLine("3 - Send Money");
                Console.WriteLine("4 - Receive Money");
                

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter amount to deposit: ");
                        deposit = Convert.ToDouble(Console.ReadLine());

                        if (deposit > 0)
                        {
                            balance += deposit;
                            Console.WriteLine("Deposit successful!");
                            Console.WriteLine("Updated Balance: ₱" + balance);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount!");
                        }
                        break;

                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        withdraw = Convert.ToDouble(Console.ReadLine());

                        if (withdraw > 0 && withdraw <= balance)
                        {
                            balance -= withdraw;
                            Console.WriteLine("Withdrawal successful!");
                            Console.WriteLine("Updated Balance: ₱" + balance);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Amount!");
                        }
                        break;



                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

            } while (choice != 4;){

            }
        }
    }
}