using System;
using System.Collections.Generic;
using System.Linq;

var sortedScores = new SortedDictionary<int, string>();

// Step 1: Add 10 players with different scores.
sortedScores.Add(850, "Alice");
sortedScores.Add(720, "Bob");
sortedScores.Add(950, "Charlie");
sortedScores.Add(600, "Diana");
sortedScores.Add(880, "Ethan");
sortedScores.Add(450, "Fiona");
sortedScores.Add(790, "George");
sortedScores.Add(990, "Hannah");
sortedScores.Add(560, "Isaac");
sortedScores.Add(680, "Julia");

// Step 2: Print the top three.
Console.WriteLine("=== Task 1: Top three ===");
DisplayScores(sortedScores.Reverse().Take(3));

// Step 3: Print the bottom three.
Console.WriteLine("=== Task 1: Bottom three ===");
DisplayScores(sortedScores.Take(3));

// Step 4: Find and remove a player by name.
string leavingPlayer = "Diana";
int? scoreToRemove = null;

foreach (var entry in sortedScores)
{
    if (entry.Value == leavingPlayer)
    {
        scoreToRemove = entry.Key;
        break;
    }
}

if (scoreToRemove.HasValue)
{
    sortedScores.Remove(scoreToRemove.Value);
}

Console.WriteLine("=== Task 1: After removing Diana ===");
DisplayScores(sortedScores.Reverse());
Console.WriteLine($"Remaining players: {sortedScores.Count}");
Console.WriteLine();

// Step 5: Same score, different name.
Console.WriteLine("=== Task 1: Duplicate score ===");

try
{
    sortedScores.Add(850, "Kevin");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
}

Console.WriteLine();

// Step 6: Different score, same name.
Console.WriteLine("=== Task 1: Duplicate name ===");
sortedScores.Add(1000, "Alice");
Console.WriteLine("Added Alice with score 1000 successfully.");

DisplayScores(
    sortedScores.Where(entry => entry.Value == "Alice")
);

static void DisplayScores(
    IEnumerable<KeyValuePair<int, string>> entries)
{
    foreach (var entry in entries)
    {
        Console.WriteLine($"Score: {entry.Key}, Player: {entry.Value}");
    }

    Console.WriteLine();
}
Console.WriteLine("=== Task 2: Player rankings ===");

Player[] players =
{
    new Player("Alice"),
    new Player("Bob"),
    new Player("Charlie"),
    new Player("Diana"),
    new Player("Ethan"),
    new Player("Fiona"),
    new Player("George"),
    new Player("Hannah"),
    new Player("Isaac"),
    new Player("Julia")
};

ScoreEntry[] scoreEntries =
{
    new ScoreEntry(850, 5),
    new ScoreEntry(720, 4),
    new ScoreEntry(950, 8),
    new ScoreEntry(600, 3),
    new ScoreEntry(880, 6),
    new ScoreEntry(450, 2),
    new ScoreEntry(790, 5),
    new ScoreEntry(990, 10),
    new ScoreEntry(560, 3),
    new ScoreEntry(850, 7)
};

// Step 2: Demonstrate the error before supplying a comparer.
Console.WriteLine("=== Task 2: Without a comparer ===");
var scoresWithoutComparer = new SortedDictionary<ScoreEntry, Player>();

try
{
    for (int i = 0; i < players.Length; i++)
    {
        scoresWithoutComparer.Add(scoreEntries[i], players[i]);
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
}

Console.WriteLine();

// Steps 3-4: Supply the comparer and add all ten entries.
var sortedScores2 =
    new SortedDictionary<ScoreEntry, Player>(new ScoreComparer());

for (int i = 0; i < players.Length; i++)
{
    sortedScores2.Add(scoreEntries[i], players[i]);
}

Console.WriteLine($"With ScoreComparer: added {sortedScores2.Count} entries.");
Console.WriteLine();

// Step 5: Reverse the ascending order to show the best players first.
Console.WriteLine("=== Task 2: Top three ===");
DisplayPlayerScores(sortedScores2.Reverse().Take(3));

// Step 6: Show the lowest scores first.
Console.WriteLine("=== Task 2: Bottom three ===");
DisplayPlayerScores(sortedScores2.Take(3));

// Step 7: Find Diana by name, then remove her using the matching key.
ScoreEntry? keyToRemove = null;

foreach (var entry in sortedScores2)
{
    if (entry.Value.Name == "Diana")
    {
        keyToRemove = entry.Key;
        break;
    }
}

if (keyToRemove != null)
{
    sortedScores2.Remove(keyToRemove);
}

Console.WriteLine("=== Task 2: After removing Diana ===");
DisplayPlayerScores(sortedScores2.Reverse());
Console.WriteLine($"Remaining entries: {sortedScores2.Count}");
Console.WriteLine();

// Step 8: Same score as Alice and Julia, but a different stage.
Console.WriteLine("=== Task 2: Same score, different stage ===");
sortedScores2.Add(new ScoreEntry(850, 9), new Player("Kevin"));
Console.WriteLine("Kevin added successfully.");
DisplayPlayerScores(
    sortedScores2.Reverse().Where(entry => entry.Key.Score == 850)
);

// Step 9: Reuse the exact Alice object with a different score.
Console.WriteLine("=== Task 2: Same Player object, different score ===");
sortedScores2.Add(new ScoreEntry(1100, 11), players[0]);
Console.WriteLine("The same Alice object was added with a new key.");
DisplayPlayerScores(
    sortedScores2.Reverse().Where(entry => entry.Value == players[0])
);

static void DisplayPlayerScores(
    IEnumerable<KeyValuePair<ScoreEntry, Player>> entries)
{
    foreach (var entry in entries)
    {
        Console.WriteLine(
            $"Player: {entry.Value.Name}, " +
            $"Score: {entry.Key.Score}, " +
            $"Stage: {entry.Key.StageReached}"
        );
    }

    Console.WriteLine();
}

// Task 3: Reuse players from Task 2 with integer priorities.
Console.WriteLine("=== Task 3: Integer priorities ===");
var eventQueue = new PriorityQueue<Player, int>();

eventQueue.Enqueue(players[0], 3); // Alice
eventQueue.Enqueue(players[1], 1); // Bob
eventQueue.Enqueue(players[2], 2); // Charlie

Console.WriteLine("Test 1: Different priorities (lower number first)");
DisplayIntegerQueue(eventQueue);

Console.WriteLine("Test 2: Duplicate priority (Bob and Diana both have 1)");
eventQueue.Enqueue(players[0], 3);
eventQueue.Enqueue(players[1], 1);
eventQueue.Enqueue(players[2], 2);
eventQueue.Enqueue(players[3], 1);

Console.WriteLine($"Entries accepted: {eventQueue.Count}");
DisplayIntegerQueue(eventQueue);
Console.WriteLine("Equal priorities are allowed; their relative order is not guaranteed.");
Console.WriteLine();

// Task 4: Use the enum's numeric values as priorities.
Console.WriteLine("=== Task 4: Enum priorities ===");
var eventQueue2 = new PriorityQueue<Player, Priority>();

eventQueue2.Enqueue(players[0], Priority.LowPriority);
eventQueue2.Enqueue(players[1], Priority.HighPriority);
eventQueue2.Enqueue(players[2], Priority.MediumPriority);

Console.WriteLine("Test 1: HighPriority (0), MediumPriority (1), LowPriority (2)");
DisplayEnumQueue(eventQueue2);

Console.WriteLine("Test 2: Duplicate priority (Bob and Diana both have HighPriority)");
eventQueue2.Enqueue(players[0], Priority.LowPriority);
eventQueue2.Enqueue(players[1], Priority.HighPriority);
eventQueue2.Enqueue(players[2], Priority.MediumPriority);
eventQueue2.Enqueue(players[3], Priority.HighPriority);

Console.WriteLine($"Entries accepted: {eventQueue2.Count}");
DisplayEnumQueue(eventQueue2);
Console.WriteLine("Equal priorities are allowed; their relative order is not guaranteed.");
Console.WriteLine();

static void DisplayIntegerQueue(PriorityQueue<Player, int> queue)
{
    // TryDequeue removes the next player and also returns their priority.
    while (queue.TryDequeue(out Player? player, out int priority))
    {
        Console.WriteLine($"Priority: {priority}, Player: {player.Name}");
    }

    Console.WriteLine($"Remaining in queue: {queue.Count}");
    Console.WriteLine();
}

static void DisplayEnumQueue(PriorityQueue<Player, Priority> queue)
{
    while (queue.TryDequeue(out Player? player, out Priority priority))
    {
        Console.WriteLine($"Priority: {priority}, Player: {player.Name}");
    }

    Console.WriteLine($"Remaining in queue: {queue.Count}");
    Console.WriteLine();
}

