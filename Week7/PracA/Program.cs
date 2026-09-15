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
