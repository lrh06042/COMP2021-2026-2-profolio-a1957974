namespace PracA;

internal class Program
{
    static void Main(string[] args)
    {
        string task = args.Length == 0 ? "all" : args[0].ToLowerInvariant();
        if (args.Length > 1 || (task != "all" && task != "task1" && task != "task2" && task != "task3"))
        {
            Console.Error.WriteLine("Usage: dotnet run -- [all|task1|task2|task3]");
            Environment.ExitCode = 1;
            return;
        }

        Graph graph = new Graph();

        Node A = new Node("A");
        Node B = new Node("B");
        Node C = new Node("C");
        Node D = new Node("D");
        Node E = new Node("E");
        Node F = new Node("F");
        Node G = new Node("G");
        Node H = new Node("H");
        Node I = new Node("I");

        graph.AddEdge(A, B);
        graph.AddEdge(A, C);
        graph.AddEdge(B, D);
        graph.AddEdge(B, E);
        graph.AddEdge(C, F);
        graph.AddEdge(D, G);
        graph.AddEdge(E, H);
        graph.AddEdge(F, I);

        if (task == "all" || task == "task1")
        {
            Console.WriteLine("Task 1: Building a Graph");
            Console.WriteLine("Node: adjacent nodes");
            Console.WriteLine(graph.ToString());
        }

        if (task == "all" || task == "task2")
        {
            Console.WriteLine("\nTask 2: BFS - initial graph");
            PrintBfs(A, I);
        }

        // Match the Task 2 adjusted diagram: remove F-I in both directions.
        F.Edges.Remove(I);
        I.Edges.Remove(F);
        graph.AddEdge(G, H);
        graph.AddEdge(H, I);

        if (task == "all" || task == "task2")
        {
            Console.WriteLine("\nTask 2: BFS - adjusted graph");
            PrintBfs(A, I);
        }

        if (task != "all" && task != "task3") return;

        // Restore the original graph before starting Task 3.
        G.Edges.Remove(H);
        H.Edges.Remove(G);
        H.Edges.Remove(I);
        I.Edges.Remove(H);
        graph.AddEdge(F, I);

        Console.WriteLine("\nTask 3: DFS - initial graph");
        PrintDfs(A, I);

        // Task 3 keeps F-I and adds these five undirected edges.
        graph.AddEdge(D, E);
        graph.AddEdge(E, F);
        graph.AddEdge(F, H);
        graph.AddEdge(G, H);
        graph.AddEdge(H, I);

        Console.WriteLine("\nTask 3: DFS - adjusted graph");
        PrintDfs(A, I);

        Console.WriteLine("\nTask 3 comparison: BFS - same adjusted graph");
        PrintBfs(A, I);
    }

    static void PrintBfs(Node start, Node target)
    {
        var (checkedNodes, path) = GraphSearch.BreadthFirstSearch(start, target);
        Console.WriteLine($"Start: {start.Data}, Target: {target.Data}, Found: {path.Count > 0}");
        Console.WriteLine("Checked (including target): " +
            string.Join(" -> ", checkedNodes.Select(node => node.Data)));
        Console.WriteLine("Path: " + (path.Count == 0 ? "No path found" :
            string.Join(" -> ", path.Select(node => node.Data))));
    }

    static void PrintDfs(Node start, Node target)
    {
        var (checkedNodes, path) = GraphSearch.DepthFirstSearch(start, target);
        Console.WriteLine($"Start: {start.Data}, Target: {target.Data}, Found: {path.Count > 0}");
        Console.WriteLine("Checked (including target): " +
            string.Join(" -> ", checkedNodes.Select(node => node.Data)));
        Console.WriteLine("Path: " + (path.Count == 0 ? "No path found" :
            string.Join(" -> ", path.Select(node => node.Data))));
    }
}
