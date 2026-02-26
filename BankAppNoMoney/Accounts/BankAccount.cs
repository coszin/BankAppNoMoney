using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class BankAccount : AccountBase
{
    internal BankAccount(string accountNumber, string accountName = "", decimal startingBalance = 0)
    {
        AccountNumber = accountNumber;
        AccountName = accountName;
        StartingBalance = startingBalance;
    }

    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}

