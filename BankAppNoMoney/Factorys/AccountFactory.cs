using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;
using BankAppNoMoney.Models;
using BankAppNoMoney.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAppNoMoney.Factorys;

internal static class AccountFactory
{
    internal static AccountBase CreateAccount(AccountDetails accountDetails)
    {
        switch (accountDetails.AccountType)
        {
            case AccountType.BankAccount:
                    break;
            case AccountType.IskAccount:
                    break;
            case AccountType.UddevallaAccount:
                    break;
            default:
                break;
        }
        return default;
    }
}
