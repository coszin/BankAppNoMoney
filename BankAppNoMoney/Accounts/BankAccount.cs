using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class BankAccount : AccountBase
{
    internal BankAccount(string accountNumber, string accountName = "")
    {
        AccountNumber = accountNumber;
        AccountName = accountName;
    }

    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}

