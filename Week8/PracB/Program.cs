using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

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

        Console.WriteLine("Task 1: Basic File I/O");
        Console.WriteLine($"Working directory: {Directory.GetCurrentDirectory()}");
        Console.WriteLine($"File location: {Path.GetFullPath(path)}");

        try
        {
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
}
