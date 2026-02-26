using BankAppNoMoney.Base;
using BankAppNoMoney.Accounts;
using System.Linq;
using System.Threading;

namespace BankAppNoMoney;

internal class Bank
{
    private List<AccountBase> accounts = [];

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

    internal void ShowBankMenu()
    {
        Menus menu = new(this);
        menu.ShowBankMenu();
    }
}
