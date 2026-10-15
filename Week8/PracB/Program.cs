using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

namespace PracB;

internal class Program
{
    public static void FileWriter<T>(string path, T objectToWrite) where T : class
    {
        string jsonString = JsonSerializer.Serialize(objectToWrite,
            new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("JSON to save:");
        Console.WriteLine(jsonString);
        File.WriteAllText(path, jsonString);
    }

    public static T FileReader<T>(string path) where T : class
    {
        string jsonString = File.ReadAllText(path);
        T? loadedObject = JsonSerializer.Deserialize<T>(jsonString);
        if (loadedObject is null)
        {
            throw new JsonException("The JSON did not contain an object.");
        }
        return loadedObject;
    }

    static void Main(string[] args)
    {
        string path = args.Length > 0 ? args[0] : @"./MyTest.txt";

        try
        {
            // Run each benchmark in its own process, without Tasks 1-4 first.
            if (args.Length > 0 && args[0] == "--benchmark")
            {
                if (args.Length < 2 || (args[1] != "list" && args[1] != "linkedlist"))
                {
                    Console.Error.WriteLine("Usage: --benchmark list|linkedlist [output-directory]");
                    Environment.ExitCode = 1;
                    return;
                }
                string outputDirectory = args.Length > 2 ? args[2] : ".";
                if (args[1] == "list")
                {
                    List<Player> benchmarkPlayers = new List<Player>();
                    for (int i = 0; i < 10_000; i++)
                    {
                        benchmarkPlayers.Add(new Player($"Player {i}"));
                    }
                    RunBenchmark(benchmarkPlayers,
                        Path.Combine(outputDirectory, "Task5-List.json"), "List<Player>");
                }
                else
                {
                    LinkedList<Player> benchmarkPlayers = new LinkedList<Player>();
                    for (int i = 0; i < 10_000; i++)
                    {
                        benchmarkPlayers.AddLast(new Player($"Player {i}"));
                    }
                    RunBenchmark(benchmarkPlayers,
                        Path.Combine(outputDirectory, "Task5-LinkedList.json"), "LinkedList<Player>");
                }
                return;
            }

            Console.WriteLine("Task 1: Basic File I/O");
            Console.WriteLine($"Working directory: {Directory.GetCurrentDirectory()}");
            Console.WriteLine($"File location: {Path.GetFullPath(path)}");
            // Task 1, step 4: always write, replacing any existing contents.
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine("Hello World");
            }

            // Task 1, step 5: read the whole file instead of using StreamReader.
            string text = File.ReadAllText(path);
            Console.WriteLine("Text read from the file:");
            Console.Write(text);

            Console.WriteLine();
            Console.WriteLine("Task 2: Serialise and Deserialise a List");
            Console.WriteLine("Task 3: Using generic FileWriter and FileReader methods");
            // Keep the JSON beside the Task 1 text file.
            string jsonPath = Path.Combine(Path.GetDirectoryName(path) ?? ".", "Players.json");
            List<Player> players = new List<Player>
            {
                new Player("Alice", 100, 15, 8),
                new Player("Bob", 120, 12, 10),
                new Player("Charlie", 90, 20, 5),
                new Player("Diana", 110, 18, 7),
                new Player("Ethan", 80, 25, 4)
            };

            FileWriter<List<Player>>(jsonPath, players);
            Console.WriteLine($"JSON file location: {Path.GetFullPath(jsonPath)}");

            List<Player> loadedPlayers = FileReader<List<Player>>(jsonPath);

            Console.WriteLine($"Players loaded from disk: {loadedPlayers.Count}");
            foreach (Player player in loadedPlayers)
            {
                Console.WriteLine(player);
            }

            bool dataMatches = loadedPlayers.Count == players.Count;
            for (int i = 0; dataMatches && i < players.Count; i++)
            {
                Player original = players[i];
                Player loaded = loadedPlayers[i];
                dataMatches = original.Name == loaded.Name
                    && original.Health == loaded.Health
                    && original.Attack == loaded.Attack
                    && original.Defence == loaded.Defence;
            }
            Console.WriteLine($"All player data matches: {dataMatches}");
            if (!dataMatches)
            {
                Environment.ExitCode = 1;
            }

            Console.WriteLine();
            Console.WriteLine("Task 4: Displaying memory usage for LinkedList");
            LinkedList<Player> linkedPlayers = new LinkedList<Player>();
            foreach (Player player in players)
            {
                linkedPlayers.AddLast(player);
            }

            string linkedPath = Path.Combine(Path.GetDirectoryName(path) ?? ".",
                "LinkedPlayers.json");
            FileWriter<LinkedList<Player>>(linkedPath, linkedPlayers);
            LinkedList<Player> loadedLinkedPlayers = FileReader<LinkedList<Player>>(linkedPath);

            bool linkedDataMatches = loadedLinkedPlayers.Count == players.Count;
            int playerIndex = 0;
            foreach (Player loaded in loadedLinkedPlayers)
            {
                if (!linkedDataMatches) break;
                Player original = players[playerIndex++];
                linkedDataMatches = original.Name == loaded.Name
                    && original.Health == loaded.Health
                    && original.Attack == loaded.Attack
                    && original.Defence == loaded.Defence;
            }
            if (!linkedDataMatches)
            {
                Environment.ExitCode = 1;
            }

            GC.Collect();
            long heapSizeBytes = GC.GetGCMemoryInfo().HeapSizeBytes;
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                long peakWorkingSet64 = currentProcess.PeakWorkingSet64;
                Console.WriteLine("Task 4 results: LinkedList<Player>");
                Console.WriteLine($"JSON file location: {Path.GetFullPath(linkedPath)}");
                Console.WriteLine($"LinkedList players loaded from disk: {loadedLinkedPlayers.Count}");
                Console.WriteLine($"All LinkedList player data matches: {linkedDataMatches}");
                Console.WriteLine($"HeapSizeBytes: {heapSizeBytes:N0} bytes");
                Console.WriteLine($"PeakWorkingSet64: {peakWorkingSet64:N0} bytes");
                Console.WriteLine("These are whole-application metrics, not the LinkedList's size alone.");
            }

            // Keep the collections alive until after the memory measurement.
            GC.KeepAlive(players);
            GC.KeepAlive(loadedPlayers);
            GC.KeepAlive(linkedPlayers);
            GC.KeepAlive(loadedLinkedPlayers);
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"JSON error: {ex.Message}");
            Environment.ExitCode = 1;
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"File I/O error ({ex.GetType().Name}): {ex.Message}");
            Environment.ExitCode = 1;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.Error.WriteLine($"File access denied: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }

    static void RunBenchmark<T>(T original, string path, string collectionName)
        where T : class, IEnumerable<Player>
    {
        // Keep FileWriter unchanged, but avoid printing 10,000 JSON objects.
        TextWriter consoleOutput = Console.Out;
        try
        {
            Console.SetOut(TextWriter.Null);
            FileWriter<T>(path, original);
        }
        finally
        {
            Console.SetOut(consoleOutput);
        }

        T loaded = FileReader<T>(path);
        int count = 0;
        foreach (Player player in loaded)
        {
            if (player.Name != $"Player {count}" || player.Health != 100
                || player.Attack != 10 || player.Defence != 5)
            {
                throw new InvalidDataException("The loaded player data did not match.");
            }
            count++;
        }
        if (count != 10_000)
        {
            throw new InvalidDataException("Expected 10,000 players after loading.");
        }

        GC.Collect();
        long heapSizeBytes = GC.GetGCMemoryInfo().HeapSizeBytes;
        using (Process process = Process.GetCurrentProcess())
        {
            long peakWorkingSet64 = process.PeakWorkingSet64;
            Console.WriteLine($"Task 5: {collectionName}");
            Console.WriteLine($"Runtime: {Environment.Version}");
            Console.WriteLine($"Players created: {count:N0}");
            Console.WriteLine($"Players loaded from disk: {count:N0}");
            Console.WriteLine("All player data matches: True");
            Console.WriteLine($"HeapSizeBytes: {heapSizeBytes:N0} bytes");
            Console.WriteLine($"PeakWorkingSet64: {peakWorkingSet64:N0} bytes");
            Console.WriteLine("Original and loaded collections are both alive.");
            Console.WriteLine("Whole-process measurements; each collection type runs in a fresh process.");
            Console.WriteLine($"JSON file: {Path.GetFullPath(path)}");
        }
        GC.KeepAlive(original);
        GC.KeepAlive(loaded);
    }
}
