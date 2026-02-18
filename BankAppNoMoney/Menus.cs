using BankAppNoMoney;
using BankAppNoMoney.Accounts;
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
                List<string> menuItems = new List<string>
                {
                    "Skapa konto",
                    "Ta bort konto",
                    "Visa konton",
                    "Hantera konto",
                    "Avsluta"
                };

                int index = 0;
                int choice = this.menu.PrintMenu(menuItems, index);

                switch (choice)
                {
                    case 0:
                        CreateAccount();
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
            var accountList = bank.GetAccounts();
            if (accountList.Count == 0)
            {
                Console.WriteLine("Inga konton hittades. Tryck valfri tangent för att fortsätta...");
                Console.ReadKey(true);
                return;
            }

            List<string> pairs = accountList
                .Select(a => $"AccountName: {(string.IsNullOrWhiteSpace(a.AccountName) ? "(utan namn)" : a.AccountName)} - AccountNumber: {a.AccountNumber}")
                .ToList();

            int accountIndex = this.menu.PrintMenu(pairs, 0);
            if (accountIndex < 0 || accountIndex >= pairs.Count)
            {
                Console.WriteLine("Invalid selection. Tryck valfri tangent...");
                Console.ReadKey(true);
                return;
            }

            var selectedAccount = accountList[accountIndex];

            subMenu.subMenuOptions(selectedAccount);
        }

        //ALLA KONTO OCH DERAS SALDO, OCH HANTERAR TOMMA KONTONAMN
        public void ShowAccounts()
        {
            Console.Clear();

            var accountList = bank.GetAccounts();
            if (accountList.Count == 0)
            {
                Console.WriteLine("Inga konton hittades.");
            }
            else
            {
                Console.WriteLine("Lista över konton:");
                for (int i = 0; i < accountList.Count; i++)
                {
                    var acc = accountList[i];
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
            var accountList = bank.GetAccounts();
            if (accountList.Count == 0)
            {
                Console.WriteLine("No accounts could be found, press any key to continue...");
                Console.ReadKey(true);
                return;
            }

            List<string> pairs = accountList
                .Select(a => $"AccountName: {(string.IsNullOrWhiteSpace(a.AccountName) ? "(utan namn)" : a.AccountName)} - AccountNumber: {a.AccountNumber}")
                .ToList();

            int choice = menu.PrintMenu(pairs, 0);
            if (choice < 0 || choice >= pairs.Count)
            {
                Console.WriteLine("Invalid selection. Tryck valfri tangent...");
                Console.ReadKey(true);
                return;
            }

            var accountToDelete = accountList[choice];
            bank.RemoveAccount(accountToDelete.Id);
            Console.WriteLine("Konto borttaget. Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
        }

        //HANTERAR SKAPANDE AV KONTO, OCH HANTERAR TOMMA KONTONAMN
        public void CreateAccount()
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
