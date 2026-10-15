using System;
using System.IO;

namespace PracB;

internal class Program
{
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
