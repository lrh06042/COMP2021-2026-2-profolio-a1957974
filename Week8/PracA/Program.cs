using System.Collections;
using System.Linq;
using System.Diagnostics;
using System.Text;

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
        RunTask2();
        RunTask3();
    }

    static void RunTask2()
    {
        Console.WriteLine("\nTask 2: String concatenation vs StringBuilder");
        Console.WriteLine("Times are in milliseconds (ms).");
        Console.WriteLine($"{"Characters",12} {"Append (+=)",16} {"Build",16} {"Outputs match",16}");

        int[] loopSizes = { 10_000, 50_000, 100_000 };
        foreach (int count in loopSizes)
        {
            // Append: repeatedly create a new string.
            Stopwatch appendWatch = new Stopwatch();
            string aOutput = "";
            appendWatch.Start();
            for (int i = 0; i < count; i++)
            {
                aOutput += "z";
            }
            appendWatch.Stop();

            // Build: append the same text using StringBuilder.
            Stopwatch buildWatch = new Stopwatch();
            StringBuilder bOutput = new StringBuilder();
            buildWatch.Start();
            for (int i = 0; i < count; i++)
            {
                bOutput.Append("z");
            }
            buildWatch.Stop();

            // Validate and print after timing so these operations are not measured.
            bool outputsMatch = aOutput.Length == count && aOutput == bOutput.ToString();
            if (!outputsMatch)
            {
                throw new InvalidOperationException("The two methods produced different outputs.");
            }

            Console.WriteLine($"{count,12:N0} {appendWatch.Elapsed.TotalMilliseconds,16:F4} {buildWatch.Elapsed.TotalMilliseconds,16:F4} {outputsMatch,16}");
        }
    }
    static void RunTask3()
    {
        int iterations = 40;
        List<double> appendElapsedList = new List<double>();
        List<double> builderElapsedList = new List<double>();
        List<double> sizes = new List<double>();
        List<string> results = new List<string> { "Characters,AppendMilliseconds,BuildMilliseconds" };

        Console.WriteLine("\nTask 3: Timing increasing input sizes");
        Console.WriteLine($"{"Characters",12} {"Append (ms)",16} {"Build (ms)",16}");
        Stopwatch totalWatch = Stopwatch.StartNew();

        for (int pass = 1; pass <= iterations; pass++)
        {
            int count = pass * 10_000;
            string aOutput = "";
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                aOutput += "z";
            }
            watch.Stop();
            double appendMs = watch.Elapsed.TotalMilliseconds;
            appendElapsedList.Add(appendMs);

            StringBuilder bOutput = new StringBuilder();
            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                bOutput.Append("z");
            }
            watch.Stop();
            double buildMs = watch.Elapsed.TotalMilliseconds;
            builderElapsedList.Add(buildMs);
            sizes.Add(count);

            // Each pass starts empty; validation and printing are outside timing.
            if (aOutput.Length != count || aOutput != bOutput.ToString())
            {
                throw new InvalidOperationException("Task 3 outputs did not match.");
            }
            Console.WriteLine($"{count,12:N0} {appendMs,16:F4} {buildMs,16:F4}");
            results.Add(FormattableString.Invariant($"{count},{appendMs:F4},{buildMs:F4}"));
        }
        totalWatch.Stop();

        // Locate Week8 relative to bin/Release/net11.0, regardless of terminal location.
        string outputDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../Attachments"));
        Directory.CreateDirectory(outputDirectory);
        SaveTimingPlot(sizes.ToArray(), appendElapsedList.ToArray(),
            "Append: repeated string concatenation", Path.Combine(outputDirectory, "appendPlot.png"));
        SaveTimingPlot(sizes.ToArray(), builderElapsedList.ToArray(),
            "Build: StringBuilder", Path.Combine(outputDirectory, "buildPlot.png"));
        File.WriteAllLines(Path.Combine(outputDirectory, "Task3-timings.csv"), results);
        Console.WriteLine($"All {iterations} output comparisons passed.");
        Console.WriteLine($"Task 3 benchmark duration: {totalWatch.Elapsed.TotalSeconds:F2} seconds.");
        Console.WriteLine($"Charts and timing data saved to: {outputDirectory}");
    }

    static void SaveTimingPlot(double[] sizes, double[] elapsed, string title, string filePath)
    {
        ScottPlot.Plot plot = new ScottPlot.Plot();
        plot.Add.Scatter(sizes, elapsed);
        plot.Title(title);
        plot.XLabel("Characters appended (n)");
        plot.YLabel("Elapsed time (ms)");
        plot.Axes.Margins(bottom: 0.1, top: 0.15);
        plot.SavePng(filePath, 800, 600);
    }
}

