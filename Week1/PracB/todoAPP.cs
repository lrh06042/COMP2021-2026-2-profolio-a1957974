public class TodoApp
{
    private readonly List<string> tasks = new();

    private readonly Dictionary<string, List<int>> tags = new();

    public void Run()
    {
        Console.WriteLine("Simple To-Do Manager");
        Console.WriteLine("Commands:");
        Console.WriteLine("add [item]");
        Console.WriteLine("show");
        Console.WriteLine("remove [index]");
        Console.WriteLine("clear");
        Console.WriteLine("tag [index] [name]");
        Console.WriteLine("get-tagged [tag]");
        Console.WriteLine("exit");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Error: Please enter a command.");
                continue;
            }

            string[] parts = input.Split(' ', 3);
            string command = parts[0].ToLower();

            try
            {
                switch (command)
                {
                    case "add":
                        AddTask(input);
                        break;

                    case "show":
                        ShowTasks();
                        break;

                    case "remove":
                        RemoveTask(parts);
                        break;

                    case "clear":
                        ClearTasks();
                        break;

                    case "tag":
                        TagTask(parts);
                        break;

                    case "get-tagged":
                        GetTagged(parts);
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Error: Unknown command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void AddTask(string input)
    {
        string[] parts = input.Split(' ', 2);

        if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1]))
        {
            throw new ArgumentException("Please provide an item to add.");
        }

        tasks.Add(parts[1]);
        Console.WriteLine("Task added.");
    }

    private void ShowTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }

    private void RemoveTask(string[] parts)
    {
        if (parts.Length < 2 || !int.TryParse(parts[1], out int index))
        {
            throw new ArgumentException("Please provide a valid task index.");
        }

        index--;

        if (index < 0 || index >= tasks.Count)
        {
            throw new IndexOutOfRangeException("Task index is out of range.");
        }

        tasks.RemoveAt(index);

        foreach (List<int> indices in tags.Values)
        {
            indices.Remove(index);

            for (int i = 0; i < indices.Count; i++)
            {
                if (indices[i] > index)
                {
                    indices[i]--;
                }
            }
        }

        Console.WriteLine("Task removed.");
    }

    private void ClearTasks()
    {
        tasks.Clear();
        tags.Clear();

        Console.WriteLine("All tasks cleared.");
    }

    private void TagTask(string[] parts)
    {
        if (parts.Length < 3 || !int.TryParse(parts[1], out int index))
        {
            throw new ArgumentException("Usage: tag [index] [name]");
        }

        index--;

        if (index < 0 || index >= tasks.Count)
        {
            throw new IndexOutOfRangeException("Task index is out of range.");
        }

        string tagName = parts[2].Trim();

        if (string.IsNullOrWhiteSpace(tagName))
        {
            throw new ArgumentException("Tag name cannot be empty.");
        }

        if (!tags.ContainsKey(tagName))
        {
            tags[tagName] = new List<int>();
        }

        if (tags[tagName].Contains(index))
        {
            throw new InvalidOperationException(
                "This task already has that tag."
            );
        }

        tags[tagName].Add(index);

        Console.WriteLine($"Tag '{tagName}' added.");
    }

    private void GetTagged(string[] parts)
    {
        if (parts.Length < 2)
        {
            throw new ArgumentException("Usage: get-tagged [tag]");
        }

        string tagName = parts[1];

        if (!tags.ContainsKey(tagName))
        {
            throw new KeyNotFoundException(
                $"Tag '{tagName}' was not found."
            );
        }

        Console.WriteLine($"Tasks tagged '{tagName}':");

        foreach (int index in tags[tagName])
        {
            Console.WriteLine($"{index + 1}. {tasks[index]}");
        }
    }
}