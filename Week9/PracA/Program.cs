namespace PracA;

internal class Program
{
    static void Main(string[] args)
    {
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

        Console.WriteLine("Task 1: Building a Graph");
        Console.WriteLine("Node: adjacent nodes");
        Console.WriteLine(graph.ToString());

        Console.WriteLine("\nTask 2: BFS - initial graph");
        PrintBfs(A, I);

        // Match the Task 2 adjusted diagram: remove F-I in both directions.
        F.Edges.Remove(I);
        I.Edges.Remove(F);
        graph.AddEdge(G, H);
        graph.AddEdge(H, I);

        Console.WriteLine("\nTask 2: BFS - adjusted graph");
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
}
