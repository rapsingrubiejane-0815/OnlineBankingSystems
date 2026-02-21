namespace OnlineBankingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double balance, deposit, sendAmount, receive;
            int choice;
            string recipient;

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
                        double withdraw = Convert.ToDouble(Console.ReadLine());

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

                    case 3:
                        Console.Write("Enter recipient name: ");
                        recipient = Console.ReadLine();

                        Console.Write("Enter amount to send: ");
                        sendAmount = Convert.ToDouble(Console.ReadLine());

                        if (sendAmount > 0 && sendAmount <= balance)
                        {
                            balance -= sendAmount;
                            Console.WriteLine("Money sent to " + recipient + " successfully!");
                            Console.WriteLine("Updated Balance: ₱" + balance);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount!");
                        }
                        break;

                    case 4:
                        Console.Write("Enter amount received: ");
                        receive = Convert.ToDouble(Console.ReadLine());

                        if (receive > 0)
                        {
                            balance += receive;
                            Console.WriteLine("Money received successfully!");
                            Console.WriteLine("Updated Balance: ₱" + balance);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount!");
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