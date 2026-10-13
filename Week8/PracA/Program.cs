using System.Collections;
using System.Linq;

namespace PracA;

internal class Program
{
    static void Main(string[] args)
    {
        Hashtable scoreHash = new Hashtable();

        Player alice = new Player("Alice");
        Player bob = new Player("Bob");
        Player charlie = new Player("Charlie");
        Player diana = new Player("Diana");
        Player ethan = new Player("Ethan");

        scoreHash.Add(alice, 120);
        scoreHash.Add(bob, 250);
        scoreHash.Add(charlie, 180);
        scoreHash.Add(diana, 300);
        scoreHash.Add(ethan, 210);

        // Hashtable accepts a string key even though we intended Player keys.
        scoreHash.Add("TestPlayer", 50);
        Console.WriteLine("String key added successfully: " + scoreHash["TestPlayer"]);
        scoreHash.Remove("TestPlayer"); // Keep only players for the leaderboard.

        // Reusing the same Player object tests a duplicate key.
        try
        {
            scoreHash.Add(alice, 999);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Duplicate key rejected: ArgumentException.");
        }

        // Convert the non-generic entries, then sort by score (highest first).
        var sortedScores = scoreHash.Cast<DictionaryEntry>()
            .OrderByDescending(entry => (int)entry.Value!);

        Console.WriteLine("\nPlayer scores (descending):");
        foreach (DictionaryEntry entry in sortedScores)
        {
            Player player = (Player)entry.Key;
            int score = (int)entry.Value!;
            Console.WriteLine($"{player.Name}: {score}");
        }
    }
}
