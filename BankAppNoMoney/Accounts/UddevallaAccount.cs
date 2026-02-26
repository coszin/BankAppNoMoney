using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class UddevallaAccount : AccountBase
{
    internal UddevallaAccount(string accountNumber, string accountName = "", decimal startingBalance = 0)
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
