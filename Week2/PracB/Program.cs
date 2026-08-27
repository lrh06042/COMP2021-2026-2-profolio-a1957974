using PracB;

BankAccount account = new BankAccount("Ruihan", 1000m);

Console.WriteLine($"Owner: {account.Owner}");
Console.WriteLine($"Initial balance: ${account.Balance}");

account.Deposit(100m);
Console.WriteLine($"Deposit(decimal) - $100: ${account.Balance}");

account.Deposit(200);
Console.WriteLine($"Deposit(int) - $200: ${account.Balance}");

account.Deposit(50.5);
Console.WriteLine($"Deposit(double) - $50.5: ${account.Balance}");

account.Withdraw(150m);
Console.WriteLine($"After withdrawing $150: ${account.Balance}");