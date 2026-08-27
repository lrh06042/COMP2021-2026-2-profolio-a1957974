using PracB;

namespace PracB.Tests;

public class BankAccountTests
{
    [Fact]
    public void Constructor_SetsOwnerAndBalance()
    {
        BankAccount account = new BankAccount("Ruihan", 1000m);

        Assert.Equal("Ruihan", account.Owner);
        Assert.Equal(1000m, account.Balance);
    }

    [Fact]
    public void DepositDecimal_IncreasesBalance()
    {
        BankAccount account = new BankAccount("Ruihan", 1000m);

        account.Deposit(100m);

        Assert.Equal(1100m, account.Balance);
    }

    [Fact]
    public void DepositInt_IncreasesBalance()
    {
        BankAccount account = new BankAccount("Ruihan", 1000m);

        account.Deposit(200);

        Assert.Equal(1200m, account.Balance);
    }

    [Fact]
    public void DepositDouble_IncreasesBalance()
    {
        BankAccount account = new BankAccount("Ruihan", 1000m);

        account.Deposit(50.5);

        Assert.Equal(1050.5m, account.Balance);
    }

    [Fact]
    public void Withdraw_WithEnoughBalance_DecreasesBalance()
    {
        BankAccount account = new BankAccount("Ruihan", 1000m);

        account.Withdraw(200m);

        Assert.Equal(800m, account.Balance);
    }

    [Fact]
    public void Withdraw_WithInsufficientBalance_ThrowsException()
    {
        BankAccount account = new BankAccount("Ruihan", 100m);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
            () => account.Withdraw(200m)
        );

        Assert.Equal("Insufficient balance.", exception.Message);
        Assert.Equal(100m, account.Balance);
    }
}