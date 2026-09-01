using System;

class Program
{
    static void Main()
    {
        string[] names =
        {
            "Olivia",
            "Benjamin",
            "Ava",
            "Alexander",
            "Mia",
            "Charlotte",
            "Liam",
            "Isabella",
            "Noah",
            "Sophia"
        };

        Console.WriteLine("All names in the array:");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        string longestName = names[0];
        string shortestName = names[0];

        foreach (string name in names)
        {
            if (name.Length > longestName.Length)
            {
                longestName = name;
            }

            if (name.Length < shortestName.Length)
            {
                shortestName = name;
            }
        }

        Console.WriteLine($"\nLongest name: {longestName}");
        Console.WriteLine($"Shortest name: {shortestName}");

        Console.WriteLine("\nArray before sorting:");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        Array.Sort(names);

        Console.WriteLine("\nArray after sorting:");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("\nArray before reversing:");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        Array.Reverse(names);

        Console.WriteLine("\nArray after reversing:");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }
    }
}
