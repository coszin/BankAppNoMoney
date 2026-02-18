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

                switch (opt)
                {
                    case 0:
                        Console.Write("Enter amount to deposit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            if (depositAmount <= 0)
                            {
                                Console.WriteLine("Amount must be greater than zero. Press any key to continue...");
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                selectedAccount.Deposit(depositAmount);
                                Console.WriteLine("Deposit successful. Press any key to continue...");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Press any key to continue...");
                        }
                        Console.ReadKey(true);
                        break;

                    case 1:
                        Console.Write("Enter amount to withdraw: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                        {
                            selectedAccount.Withdraw(withdrawAmount);
                            Console.WriteLine("Withdrawal processed. Press any key to continue...");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Press any key to continue...");
                        }
                        Console.ReadKey(true);
                        break;

                    case 2:
                        decimal balance = selectedAccount.Balance();
                        Console.WriteLine($"Current balance: {balance}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;

                    case 3:
                        return; // Back to main menu

                    default:
                        Console.WriteLine("Invalid menu choice. Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                }
            } while (true);
        }

        public void SubMenuCreate(int choice)
        {
            Console.Write("Ange kontonummer: ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";
            while (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.Write("Kontonummer får inte vara tomt. Ange kontonummer: ");
                accountNumber = Console.ReadLine()?.Trim() ?? "";
            }

            Console.Write("Vill du ange ett kontonamn? (Y/N): ");
            var key = Console.ReadKey(true).Key;
            string accountName = "";
            if (key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.Write("Ange kontonamn: ");
                accountName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                Console.WriteLine();
            }

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
    }
}
