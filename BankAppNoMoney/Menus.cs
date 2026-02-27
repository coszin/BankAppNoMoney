using BankAppNoMoney;
using BankAppNoMoney.Accounts;
using BankAppNoMoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BankAppNoMoney.Base
{
    internal class Menus
    {
        private readonly MenuLogic menu;
        private readonly Bank bank;
        private readonly SubMenus subMenu;
        private readonly AccountDetails accountDetails;

        internal Menus(Bank bank)
        {
            this.menu = new MenuLogic();
            this.bank = bank ?? throw new ArgumentNullException(nameof(bank));
            this.subMenu = new SubMenus(this.bank);
        }

        //HANTERAR HUVUDMENYN, OCH HANTERAR INVALIDA VAL
        internal void ShowBankMenu()
        {
            do
            {
                List<string> menuItems =
                [
                    "Skapa konto",
                    "Ta bort konto",
                    "Visa konton",
                    "Hantera konto",
                    "Avsluta"
                ];

                int index = 0;
                int choice = this.menu.PrintMenu(menuItems, index);

                switch (choice)
                {
                    case 0:
                        CreateAccount(accountDetails);
                        break;
                    case 1:
                        DeleteAccount();
                        break;
                    case 2:
                        ShowAccounts();
                        break;
                    case 3:
                        AccountOptions();
                        break;
                    case 4:
                        Console.WriteLine("Stänger ner...");
                        Thread.Sleep(1000);
                        return; // Exit the method to close the application
                    default:
                        Console.WriteLine("Invalid menu choice. Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                }
            } while (true);
        }

        //HANTERAR DE OLIKA ALTERNATIVEN FÖR ETT KONTO, OCH HANTERAR TOMMA KONTONAMN
        public void AccountOptions()
        {
            var AccountList = bank.GetAccounts();
            if (AccountList.Count == 0)
            {
                Console.WriteLine("Inga konton hittades. Tryck valfri tangent för att fortsätta...");
                Console.ReadKey(true);
                return;
            }

            List<string> pairs = AccountList
                .Select(a => $"AccountName: {(string.IsNullOrWhiteSpace(a.AccountName) ? "(utan namn)" : a.AccountName)} - AccountNumber: {a.AccountNumber}")
                .ToList();

            int accountIndex = this.menu.PrintMenu(pairs, 0);
            if (accountIndex < 0 || accountIndex >= pairs.Count)
            {
                Console.WriteLine("Invalid selection. Tryck valfri tangent...");
                Console.ReadKey(true);
                return;
            }

            var selectedAccount = AccountList[accountIndex];

            subMenu.subMenuOptions(selectedAccount);
        }

        //ALLA KONTO OCH DERAS SALDO, OCH HANTERAR TOMMA KONTONAMN
        public void ShowAccounts()
        {
            Console.Clear();

            var AccountList = bank.GetAccounts();
            if (AccountList.Count == 0) { Console.WriteLine("Inga konton hittades."); }
            else
            {
                Console.WriteLine("Lista över konton:");
                for (int i = 0; i < AccountList.Count; i++)
                {
                    var acc = AccountList[i];
                    var name = string.IsNullOrWhiteSpace(acc.AccountName) ? "(utan namn)" : acc.AccountName;
                    Console.WriteLine($"{i + 1}. AccountNumber: {acc.AccountNumber}  -  AccountName: {name}  -  Saldo: {acc.Balance()}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
        }

        //HANTERAR BORTTAGNING AV KONTO, OCH HANTERAR TOMMA KONTONAMN
        public void DeleteAccount()
        {
            var AccountList = bank.GetAccounts();
            if (AccountList.Count == 0)
            {
                Console.WriteLine("No accounts could be found, press any key to continue...");
                Console.ReadKey(true);
                return;
            }

            List<string> ListAccounts = AccountList
                .Select(a => $"AccountName: {(string.IsNullOrWhiteSpace(a.AccountName) ? "(utan namn)" : a.AccountName)} - AccountNumber: {a.AccountNumber}")
                .ToList();
            bool flowControl = AccountRemove(AccountList, ListAccounts);
            if (!flowControl)
            {
                return;
            }
        }

        private bool AccountRemove(List<AccountBase> AccountList, List<string> ListAccounts)
        {
            int choice = menu.PrintMenu(ListAccounts, 0);
            if (choice < 0 || choice >= ListAccounts.Count)
            {
                Console.WriteLine("Invalid selection. Tryck valfri tangent...");
                Console.ReadKey(true);
                return false;
            }

            var accountToDelete = AccountList[choice];
            bank.RemoveAccount(accountToDelete.Id);
            Console.WriteLine("Konto borttaget. Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
            return true;
        }

        //HANTERAR SKAPANDE AV KONTO, OCH HANTERAR TOMMA KONTONAMN
        public void CreateAccount(AccountDetails accountDetails)
        {
            List<string> AccountType = new List<string>
            {
                "BankAccount",
                "IskAccount",
                "UddevallaAccount"
            };
            int choice = menu.PrintMenu(AccountType, 0);

            if (choice < 0 || choice >= AccountType.Count)
                throw new InvalidOperationException("Invalid menu choice");

            Console.Clear();
            subMenu.SubMenuCreate(choice);
        }
    }
}
