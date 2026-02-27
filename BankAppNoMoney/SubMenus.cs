using BankAppNoMoney;
using BankAppNoMoney.Accounts;
using BankAppNoMoney.Factorys;
using BankAppNoMoney.Models;
using BankAppNoMoney.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAppNoMoney.Base
{
    internal class SubMenus
    {
        private readonly MenuLogic menu;
        private readonly Bank bank;
        private readonly AccountType accountType;

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
                Console.ReadKey(true);
            }
        }

        public void SubMenuCreate(int choice)
        { 
            var accountDetails = new AccountDetails(Kontonamn(), KontoInfo(1).ToString(), KontoInfo(0), (AccountType)choice);
            bank.AddAccount(AccountFactory.CreateAccount(accountDetails));

            Console.WriteLine("Konto skapat. Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
        }

        private static decimal KontoInfo(int tni)
        {
            if(tni == 1) { Console.Write("Ange kontonummer: "); }
            else { Console.Write("Ange saldo: "); }
            bool TF = decimal.TryParse(Console.ReadLine()?.Trim(), out decimal KontoInfo);
            while (!TF && KontoInfo > 0)
            {
                if (tni == 1) { Console.Write("Saldo får inte vara negativt och måste vara siffror. Ange kontonummer: "); }
                else { Console.Write("Saldo får inte vara negativt och måste vara siffror: "); }
                TF = decimal.TryParse(Console.ReadLine()?.Trim(), out KontoInfo);
            }
            if (tni == 1) { return KontoInfo; }
            else { return KontoInfo; }
        }

        private static string Kontonamn()
        {
            Console.Write("Ange ett Konto namn: ");
            
            string accountName = Console.ReadLine()?.Trim() ?? "";
           
            return accountName;
        }
    }
}
