using System;

namespace CSharp.LabExercise3
{
    class Account
    {
        string accountName;
        int accountId;
        decimal accountBalance;
        int pin;
        string[] accounts = new string[10];

        public string AccountName
        {
            get { return this.accountName; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                {
                    Console.WriteLine("Account Name is Invalid");
                }
                this.accountName = value;
            }
        }

        public int AccountId
        {
            get { return this.accountId; }
            set
            {
                if (value < 1)
                {
                    Console.WriteLine("Account ID is Invalid");
                }
                this.accountId = value;
            }
        }

        public decimal AccountBalance
        {
            get { return this.accountBalance; }
            set
            {
                this.accountBalance = value;
            }
        }

        public int Pin
        {
            get { return this.pin; }
            set
            {
                if (value.ToString().Length == 0 || value.ToString().Length < 4 || value.ToString().Length > 4)
                {
                    Console.WriteLine("Invalid Pin");
                }
                this.pin = value;
            }
        }

    }

    class AmountBalance
    {
        Account account;
        public AmountBalance(Account account)
        {
            this.account = account;
        }


        public void Credit(decimal amount)
        {
            if (amount > 0)
            {
                account.AccountBalance += amount;
                Console.WriteLine("Deposit Successful!");
                Console.WriteLine("New Balance Account: {0}", account.AccountBalance);
                Console.WriteLine("Press any key to continue....");
            }
        }

        public void Debit(decimal amount)
        {

            if (amount <= account.AccountBalance)
            {
                if (amount > 1)
                {
                    if (amount % 100 == 0)
                    {
                        account.AccountBalance -= amount;
                        Console.WriteLine($"Please get your cash {amount}");
                        Console.WriteLine("Press any key to continue....");
                    }
                    else
                    {
                        Console.WriteLine("Amount must be in 100s");
                        Console.Write("Try Withdraw Again? (y/n): ");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Amount");
                    Console.Write("Try Withdraw Again? (y/n): ");
                }

            }
            else
            {
                Console.WriteLine("Insufficient Balance.");
                Console.Write("Try Withdraw Again? (y/n): ");
            }


        }

        public void ParseWithdraw(string amount)
        {
            decimal finalAmount;
            bool parseSuccess = decimal.TryParse(amount, out finalAmount);
            if (parseSuccess)
            {
                Debit(finalAmount);
            }
            else
            {
                Console.WriteLine("This is not a Number!");
                Console.Write("Try Withdraw Again? (y/n): ");
            }
        }

        public void ParseDeposit(string amount)
        {
            decimal finalAmount;
            bool parseSuccess = decimal.TryParse(amount, out finalAmount);
            if (parseSuccess)
            {
                Credit(finalAmount);
            }
            else
            {
                Console.WriteLine("This is not a Number!");
                Console.Write("Try to Deposit Again? (y/n): ");
            }
        }

    }

    class ATM
    {

        Account account;
        public ATM(Account account)
        {
            this.account = account;
        }

        public void DisplayOption()
        {
            Console.WriteLine("1.Check Balance");
            Console.WriteLine("2.Withdraw");
            Console.WriteLine("3.Deposit");
            Console.WriteLine("4.Exit");
            Console.Write("Enter your choice: ");
        }

        public void DisplayBalance()
        {
            Console.WriteLine("{0}", account.AccountBalance);
        }

        public void DisplayWithdraw()
        {
            Console.Write("Enter amount you want to Withdraw:");
        }

        public void DisplayDeposit()
        {
            Console.Write("Enter amount you want to Deposit:");
        }

        public void ViewBalance()
        {
            Console.Write("View Balance? (y/n): ");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account
            {

                AccountName = "John Doe",
                AccountId = 10,
                AccountBalance = 0,
                Pin = 1111,
            };
            ATM aTM = new ATM(account);
            AmountBalance amountbalance = new AmountBalance(account);
            while (true)
            {
                aTM.DisplayOption();
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        aTM.DisplayBalance();
                        break;
                    case 2:
                        aTM.DisplayWithdraw();
                        amountbalance.ParseWithdraw(Console.ReadLine());
                        string again = Console.ReadLine();
                        if (again == "y" || again == "Y")
                        {
                            goto case 2;
                        }
                        aTM.ViewBalance();
                        string balance = Console.ReadLine();
                        if (balance == "y" || balance == "Y")
                        {
                            goto case 1;
                        }
                        break;
                    case 3:
                        aTM.DisplayDeposit();
                        amountbalance.ParseDeposit(Console.ReadLine());
                        string tryagain = Console.ReadLine();
                        if (tryagain == "y" || tryagain == "Y")
                        {
                            goto case 3;
                        }
                        break;
                    case 4:
                        Console.WriteLine("Exit..");
                        Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("\nInvalid Selection! Press Any Key To Try Again! . . . ");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
