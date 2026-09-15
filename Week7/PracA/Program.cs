using System;
using System.Collections.Generic;

SortedSet<int> scores = new();
Random random = new();

// Keep generating until there are 10 unique scores.
while (scores.Count < 10)
{
    scores.Add(random.Next(1, 1001));
}

Console.WriteLine("=== Test 1: 10 random scores ===");
DisplayScores(scores);

Console.WriteLine("=== Test 2: Add a new highest score (1100) ===");
scores.Add(1100);
DisplayScores(scores);

Console.WriteLine("=== Test 3: Add a duplicate score (1100) ===");
bool added = scores.Add(1100);
Console.WriteLine($"Duplicate added: {added}");
DisplayScores(scores);

static void DisplayScores(SortedSet<int> scores)
{
    if (scores.Count == 0)
    {
        Console.WriteLine("No scores available.");
        return;
    }

    Console.WriteLine($"Scores: {string.Join(", ", scores)}");
    Console.WriteLine($"Highest score: {scores.Max}");
    Console.WriteLine($"Lowest score: {scores.Min}");
    Console.WriteLine($"Count: {scores.Count}");
    Console.WriteLine();
}
Console.WriteLine("=== Task 2: BankAccount without a comparer ===");

List<BankAccount> accounts = new();

for (int i = 1; i <= 10; i++)
{
    accounts.Add(new BankAccount(
        $"Owner{i:D2}", random.Next(10000, 500001) / 100m));
}

SortedSet<BankAccount> sortedBankSet = new(new BankComparer());

foreach (BankAccount account in accounts)
{
    sortedBankSet.Add(account);
}
Console.WriteLine("=== Accounts Test 1: 10 accounts ===");
DisplayAccounts(sortedBankSet);

Console.WriteLine("=== Accounts Test 2: Add highest balance ===");
sortedBankSet.Add(new BankAccount("TopOwner", 10000m));
DisplayAccounts(sortedBankSet);

Console.WriteLine("=== Accounts Test 3: Add duplicate ===");
bool accountAdded = sortedBankSet.Add(
    new BankAccount("TopOwner", 10000m));
Console.WriteLine($"Duplicate added: {accountAdded}");
DisplayAccounts(sortedBankSet);

static void DisplayAccounts(SortedSet<BankAccount> accounts)
{
    foreach (BankAccount account in accounts)
    {
        Console.WriteLine($"{account.Owner,-12} {account.Balance,10:F2}");
    }

    Console.WriteLine($"Count: {accounts.Count}");
    Console.WriteLine();
}