using BankAppNoMoney;
using BankAppNoMoney.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAppNoMoney.Base
{
    internal class SubMenus
    {
        private readonly MenuLogic menu;
        private readonly Bank bank;

        internal SubMenus(Bank bank)
        {
            this.menu = new MenuLogic();
            this.bank = bank ?? throw new ArgumentNullException(nameof(bank));
        }

        public void subMenuOptions(AccountBase selectedAccount)
        {
            do
            {
                List<string> options = new List<string>
                {
                    "Deposit",
                    "Withdraw",
                    "Balance",
                    "Back to main menu"
                };

                int opt = menu.PrintMenu(options, 0);
                bool flowControl = SwitchOptions(selectedAccount, opt);
                if (!flowControl)
                {
                    return;
                }
            } while (true);
        }

        private static bool SwitchOptions(AccountBase selectedAccount, int opt)
        {
            switch (opt)
            {
                case 0:
                    Case0(selectedAccount);
                    break;
                case 1:
                    bool TF = Case1(selectedAccount);
                    break;
                case 2:
                    Case2(selectedAccount);
                    break;
                case 3:
                    return false; // Back to main 
                default:
                    Case3();
                    break;
            } return true;
        }

        private static void Case3()
        {
            Console.WriteLine("Invalid menu choice. Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void Case2(AccountBase selectedAccount)
        {
            decimal balance = selectedAccount.Balance();
            Console.WriteLine($"Current balance: {balance}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static bool Case1(AccountBase selectedAccount)
        {
            bool TF;
            Console.Write("Enter amount to withdraw: ");
            TF = decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount);
            if (withdrawAmount > 0 && TF == true)
            {
                selectedAccount.Withdraw(withdrawAmount);
                Console.WriteLine("Withdrawal processed. Press any key to continue...");
            }
            else
            {
                Console.WriteLine("Invalid amount. Press any key to continue...");
            }
            Console.ReadKey(true);
            return TF;
        }

        private static void Case0(AccountBase selectedAccount)
        {
            Console.Write("Enter amount to deposit: ");
            bool TF = decimal.TryParse(Console.ReadLine(), out decimal depositAmount);
            if (depositAmount < 0 || !TF)
            {
                Console.WriteLine("Amount must be greater than zero. Press any key to continue...");
                Console.ReadKey(true);
            }
            else
            {
                selectedAccount.Deposit(depositAmount);
                Console.WriteLine("Deposit successful. Press any key to continue...");
            }
            Console.ReadKey(true);
        }

        public void SubMenuCreate(int choice)
        { 
            string accountNumber = Kontonummer();
            string accountName = Kontonamn();

            if (choice == 0)
            {
                bank.AddAccount(new BankAccount(accountNumber, accountName));
            }
            else if (choice == 1)
            {
                bank.AddAccount(new IskAccount(accountNumber, accountName));
            }
            else if (choice == 2)
            {
                bank.AddAccount(new UddevallaAccount(accountNumber, accountName));
            }
            Console.WriteLine("Konto skapat. Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
        }

        private static string Kontonummer()
        {
            Console.Write("Ange kontonummer: ");
            bool TF = decimal.TryParse(Console.ReadLine()?.Trim(), out decimal accountNumber);
            while (!TF)
            {
                Console.Write("Kontonummer får inte vara tomt och måste vara sifror. Ange kontonummer: ");
                TF = decimal.TryParse(Console.ReadLine()?.Trim(), out accountNumber);
            }
            return accountNumber.ToString();
        }

        private static string Kontonamn()
        {
            string accountName;
            Console.Write("Vill du ange ett kontonamn? (Y/N): ");
            var key = Console.ReadKey(true).Key;
            accountName = "";
            if (key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.Write("Ange kontonamn: ");
                accountName = Console.ReadLine()?.Trim() ?? "";
            }
            else { Console.WriteLine(); }

            return accountName;
        }
    }
}
