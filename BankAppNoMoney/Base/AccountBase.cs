namespace BankAppNoMoney.Base;

internal abstract class AccountBase
{
    internal Guid Id { get; set; } = Guid.NewGuid();
    protected decimal StartingBalance { get; set; } = 0;
    internal string AccountName { get; set; } = string.Empty;
    internal string AccountNumber { get; set; } = string.Empty;
    internal decimal InterestRate { get; set; } = 0;

    protected List<BankTransaction> bankTransactions = [];

    internal abstract decimal Balance();

    internal virtual void  Deposit(decimal amount)
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
/*● Banker kollar inte saldot i december 2025 och räknar ut ränta för hela året.
● Utan kollar saldot för varje dag och räknar ut summan
● Använd 365 dagar.
● Lägg denna funktion i basklassen
● Ge den ett bra namn och skall returnera ett decimaltal.
● Använd testdata. Lägg in 10 insättningar under året, sätt en ränta.
● Testdata ska inte vara random, blir svårare vid felsökning.*/