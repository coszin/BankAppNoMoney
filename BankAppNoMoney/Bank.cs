using BankAppNoMoney.Base;
using BankAppNoMoney.Accounts;
using System.Linq;
using System.Threading;

namespace BankAppNoMoney;

internal class Bank
{
    private List<AccountBase> accounts = new List<AccountBase>();

    internal void AddAccount(AccountBase account)
    {
        accounts.Add(account);
    }

    internal void RemoveAccount(Guid accountId)
    {
        var account = accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null)
        {
            accounts.Remove(account);
        }
    }

    internal List<AccountBase> GetAccounts()
    {
        return accounts;
    }

    //HANTERAR HUVUDMENYN, OCH HANTERAR INVALIDA VAL
    internal void ShowBankMenu()
    {
        do
        {
            List<string> menu = new List<string>
            {
                "Skapa konto",
                "Ta bort konto",
                "Visa konton",
                "Hantera konto",
                "Avsluta"
            };

            int index = 0;
            int choice = PrintMenu(menu, index);

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
    private void AccountOptions()
    {
            var accountList = GetAccounts();
        if (accountList.Count == 0)
        {
            Console.WriteLine("Inga konton hittades. Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
            return;
        }

        List<string> pairs = accountList
            .Select(a => $"AccountName: {(string.IsNullOrWhiteSpace(a.AccountName) ? "(utan namn)" : a.AccountName)} - AccountNumber: {a.AccountNumber}")
            .ToList();

        int accountIndex = PrintMenu(pairs, 0);
        if (accountIndex < 0 || accountIndex >= pairs.Count)
        {
            Console.WriteLine("Invalid selection. Tryck valfri tangent...");
            Console.ReadKey(true);
            return;
        }

        var selectedAccount = accountList[accountIndex];

        do
        {
            List<string> options = new List<string>
            {
                "Deposit",
                "Withdraw",
                "Balance",
                "Back to main menu"
            };

            int opt = PrintMenu(options, 0);

            switch (opt)
            {
                case 0:
                    Console.Write("Enter amount to deposit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        selectedAccount.Deposit(depositAmount);
                        Console.WriteLine("Deposit successful. Press any key to continue...");
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

    //ALLA KONTO OCH DERAS SALDO, OCH HANTERAR TOMMA KONTONAMN
    private void ShowAccounts()
    {
        Console.Clear();

        var accountList = GetAccounts();
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
    private void DeleteAccount()
    {
        var accountList = GetAccounts();
        if (accountList.Count == 0)
        {
            Console.WriteLine("No accounts could be found, press any key to continue...");
            Console.ReadKey(true);
            return;
        }

        List<string> pairs = accountList
            .Select(a => $"AccountName: {(string.IsNullOrWhiteSpace(a.AccountName) ? "(utan namn)" : a.AccountName)} - AccountNumber: {a.AccountNumber}")
            .ToList();

        int choice = PrintMenu(pairs, 0);
        if (choice < 0 || choice >= pairs.Count)
        {
            Console.WriteLine("Invalid selection. Tryck valfri tangent...");
            Console.ReadKey(true);
            return;
        }

        var accountToDelete = accountList[choice];
        RemoveAccount(accountToDelete.Id);
        Console.WriteLine("Konto borttaget. Tryck valfri tangent för att fortsätta...");
        Console.ReadKey(true);
    }

    //HANTERAR SKAPANDE AV KONTO, OCH HANTERAR TOMMA KONTONAMN
    private void CreateAccount()
    {
        List<string> AccountType = new List<string>
        {
            "BankAccount",
            "IskAccount",
            "UddevallaAccount"
        };
        int choice = PrintMenu(AccountType, 0);

        if (choice < 0 || choice >= AccountType.Count)
            throw new InvalidOperationException("Invalid menu choice");

        Console.Clear();
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
            accounts.Add(new BankAccount(accountNumber, accountName));
        }
        else if (choice == 1)
        {
            accounts.Add(new IskAccount(accountNumber, accountName));
        }
        else if (choice == 2)
        {
            accounts.Add(new UddevallaAccount(accountNumber, accountName));
        }

        Console.WriteLine("Konto skapat. Tryck valfri tangent för att fortsätta...");
        Console.ReadKey(true);
    }

    // HANTERAR MENYN, OCH HANTERAR INVALIDA VAL
    static int PrintMenu(List<string> menu, int index)
    {
        if (menu == null || menu.Count == 0)
            return -1;

        int current = Math.Clamp(index, 0, menu.Count - 1);

        ConsoleKey key;
        do
        {
            Console.Clear();
            DrawMenu(menu, current);

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    current = (current - 1 + menu.Count) % menu.Count;
                    break;
                case ConsoleKey.DownArrow:
                    current = (current + 1) % menu.Count;
                    break;
                case ConsoleKey.Enter:
                    break;
                default:
                    Console.WriteLine("Use Up/Down arrows to navigate and Enter to select.");
                    Console.ReadKey(true); // Wait for user to read the message
                    break;
            }

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        return current;
    }

    // RITAR MENYN, OCH VISAR VILKET ALTERNATIV SOM ÄR VALT
    static void DrawMenu(List<string> menu, int selectedIndex)
    {
        for (int i = 0; i < menu.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.WriteLine($"< {menu[i]} >");
            }
            else
            {
                Console.WriteLine($"  {menu[i]}");
            }
        }
    }
}
