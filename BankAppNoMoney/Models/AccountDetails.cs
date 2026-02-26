using BankAppNoMoney.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAppNoMoney.Models;

internal class AccountDetails
{
    internal string AccountName { get; set; } = string.Empty;
    internal string AccountNumber { get; set; } = string.Empty;
    internal decimal StartingBalance { get; set; }
    internal AccountType AccountType { get; set; }

    public AccountDetails(string accountName, string accountNumber, decimal startingBalance, AccountType accountType) 
    { 
        AccountName = accountName;
        AccountNumber = accountNumber;
        StartingBalance = startingBalance;
        AccountType = accountType;
    }
}
