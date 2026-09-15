using System;

public class BankAccount(string owner, decimal balance)
{
    public string Owner { get; set; } = owner;
    public decimal Balance { get; set; } = balance;

    public decimal Deposit(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must not be negative.");

        return Balance += amount;
    }

    public decimal Deposit(int amount) => Deposit((decimal)amount);

    public decimal Deposit(double amount) => Deposit((decimal)amount);

    public virtual decimal Withdraw(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must not be negative.");

        if (Balance < amount)
            throw new ArgumentException("Balance is less than amount");

        return Balance -= amount;
    }

    public override string ToString()
    {
        return $"Account: {nameof(BankAccount)}\n" +
               $"Owner: {Owner}\nBalance: {Balance:F2}";
    }
}