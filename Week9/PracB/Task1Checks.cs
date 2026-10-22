namespace PARCB;

internal static class Task1Checks
{
    public static void Run()
    {
        int passed = 0;
        void Check(bool condition, string description)
        {
            if (!condition) throw new Exception("FAIL: " + description);
            Console.WriteLine("PASS: " + description); passed++;
        }
        var (horse, start, end) = HorseGraph.Create();
        var result = Dijkstra.FindPath(horse, start, end);
        Check(horse.Nodes.Count == 16 && horse.Nodes.Sum(n => n.Edges.Count) == 46, "16 nodes and 23 undirected edges");
        Check(horse.Nodes.All(n => n.Edges.All(e => e.Key.Edges.TryGetValue(n, out int w) && w == e.Value)), "Symmetric edges");
        Check(string.Join(",", result.Path.Select(n => n.Data)) == "Start,A,C,F,G,H,K,N,M,L,End" && result.Distance == 10, "Expected shortest path and cost 10");
        Check(result.Path.All(n => !n.Data.Contains("BEES")), "No bees on shortest path");
        foreach (var path in new[] { GraphSearch.BreadthFirstSearch(start, end).Path, GraphSearch.DepthFirstSearch(start, end).Path })
            Check(path.Count > 0 && path[0] == start && path[^1] == end && path.Zip(path.Skip(1)).All(p => p.First.Edges.ContainsKey(p.Second)), "Valid BFS/DFS path");
        Graph graph = new();
        Node a = new("A"), b = new("B"), c = new("C"), d = new("D"), isolated = new("Isolated");
        graph.AddEdge(a, b, 9); graph.AddEdge(a, c, 1); graph.AddEdge(c, b, 1); graph.AddEdge(b, d, 20); graph.Nodes.Add(isolated);
        Check(Dijkstra.FindPath(graph, a, d).Distance == 22, "Improved distance and stale queue entry");
        var missing = Dijkstra.FindPath(graph, a, isolated);
        Check(missing.Path.Count == 0 && missing.Distance == long.MaxValue, "Unreachable target");
        var same = Dijkstra.FindPath(graph, a, a);
        Check(same.Path.SequenceEqual(new[] { a }) && same.Distance == 0, "Start equals target");
        Graph zero = new(); zero.AddEdge(a = new("A"), b = new("B"), 0);
        Check(Dijkstra.FindPath(zero, a, b).Distance == 0, "Zero-weight edge");
        Graph large = new(); large.AddEdge(a = new("A"), b = new("B"), int.MaxValue); large.AddEdge(b, c = new("C"), int.MaxValue);
        Check(Dijkstra.FindPath(large, a, c).Distance == 2L * int.MaxValue, "Large total distance");
        bool rejected = false;
        try { large.AddEdge(a, c, -1); } catch (ArgumentOutOfRangeException) { rejected = true; }
        Check(rejected, "Reject negative edge");
        Console.WriteLine($"All {passed} checks passed.");
    }
}
