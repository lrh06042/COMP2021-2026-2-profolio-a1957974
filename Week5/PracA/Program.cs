using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Task 1: Using Arrays
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

        Console.WriteLine("=== Task 1: Using Arrays ===");
        Console.WriteLine("\nAll names in the array:");

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

        // Task 2: Using Lists
        Console.WriteLine("\n=== Task 2: Using Lists ===");

        List<string> studentNames = new List<string>
        {
            "Olivia",
            "Liam",
            "Amelia",
            "Noah",
            "Sophia",
            "Mason",
            "Ava"
        };

        // Use the required List methods
        studentNames.Add("Han");
        studentNames.Remove("Amelia");
        studentNames.Insert(1, "V");

        studentNames.AddRange(new[]
        {
            "Jason",
            "Lucia"
        });

        Console.WriteLine("\nStudent names after using List methods:");

        foreach (string studentName in studentNames)
        {
            Console.WriteLine(studentName);
        }

        // Search for a complete name
        string specificName = "Han";
        int specificNameIndex = studentNames.IndexOf(specificName);

        if (specificNameIndex >= 0)
        {
            Console.WriteLine(
                $"\nIndex of {specificName}: {specificNameIndex}"
            );
        }
        else
        {
            Console.WriteLine($"\n{specificName} was not found.");
        }

        // Search for part of a name
        string partialName = "ia";
        List<string> matchingNames = new List<string>();

        foreach (string studentName in studentNames)
        {
            if (studentName.Contains(
                partialName,
                StringComparison.OrdinalIgnoreCase
            ))
            {
                matchingNames.Add(studentName);
            }
        }

        Console.WriteLine(
            $"\nNames containing \"{partialName}\":"
        );

        foreach (string matchingName in matchingNames)
        {
            Console.WriteLine(matchingName);
        }

        // Sum the length of all names
        int totalNameLength = 0;

        foreach (string studentName in studentNames)
        {
            totalNameLength += studentName.Length;
        }

        Console.WriteLine(
            $"\nSum of all name lengths: {totalNameLength}"
        );

        // Convert the Task 1 Array into a List
        List<string> namesFromArray = new List<string>(names);

        // Add the converted names to the Task 2 List
        studentNames.AddRange(namesFromArray);

        // Convert the final List back into an Array
        string[] namesConvertedBackToArray = studentNames.ToArray();

        Console.WriteLine(
            $"\nNames converted from Task 1 array: {namesFromArray.Count}"
        );

        Console.WriteLine(
            $"Final student list count: {studentNames.Count}"
        );

        Console.WriteLine(
            $"List converted back to array: {namesConvertedBackToArray.Length}"
        );
    }
}