using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;
using BankAppNoMoney.Models;
using BankAppNoMoney.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BankAppNoMoney.Factorys;

public static class AccountFactory
{
    internal static AccountBase CreateAccount(AccountDetails accountDetails)
    {
        switch (accountDetails.AccountType)
        {
            case AccountType.BankAccount:
                return new BankAccount(accountDetails.AccountName, accountDetails.AccountNumber, accountDetails.StartingBalance);
            case AccountType.IskAccount:
                return new IskAccount(accountDetails.AccountName, accountDetails.AccountNumber, accountDetails.StartingBalance);
            case AccountType.UddevallaAccount:
                return new UddevallaAccount(accountDetails.AccountName, accountDetails.AccountNumber, accountDetails.StartingBalance);
            default:
                throw new NotImplementedException("A missing account option in factorys");
        }
    }
}
