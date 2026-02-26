using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class IskAccount : AccountBase
{
    internal IskAccount(string accountNumber, string accountName = "", decimal startingBalace = 0)
    {
        AccountNumber = accountNumber;
        AccountName = accountName;
        StartingBalance = startingBalace;
    }

    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}
