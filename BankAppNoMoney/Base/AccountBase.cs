namespace BankAppNoMoney.Base;

internal abstract class AccountBase
{
    internal Guid Id { get; set; } = Guid.NewGuid();
    protected decimal StartingBalance { get; set; } = 0;
    internal string AccountName { get; set; } = "";
    internal string AccountNumber { get; set; } = "";
    internal decimal InterestRate { get; set; } = 0;

    protected List<BankTransaction> bankTransactions = new List<BankTransaction>();

    internal abstract decimal Balance();

    internal virtual void Deposit(decimal amount)
    {
        var t = new BankTransaction
        {
            Amount = amount,
            TransactionalDate = DateTime.Now
        };
        bankTransactions.Add(t);
    }

    internal virtual void Withdraw(decimal amount)
    {
        var balance = Balance();
        if (balance < amount)
        {
            Console.WriteLine("Not enough money to withdraw");
            return;
        }

        var t = new BankTransaction
        {
            Amount = -amount,
            TransactionalDate = DateTime.Now
        };

        bankTransactions.Add(t);
    }
}
