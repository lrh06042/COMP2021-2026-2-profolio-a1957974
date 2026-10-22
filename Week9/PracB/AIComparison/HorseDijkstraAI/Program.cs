namespace HorseDijkstraAI;

internal static class Program
{
    // A matrix-based alternative: no Node objects or priority queue.
    static readonly string[] Names = { "L", "I", "F", "C", "A", "M", "J", "G", "D", "Start", "N", "K", "H", "E", "B", "End" };
    static readonly HashSet<string> Bees = new() { "I", "J", "D", "E", "B" };

    static void Main(string[] args)
    {
        var graph = CreateHorseGraph();
        if (args.Contains("--test")) { RunChecks(graph); return; }
        var (path, cost) = ShortestPath(graph, 9, 15);
        Console.WriteLine("Task 2 - AI matrix-based Dijkstra");
        Console.WriteLine("Graph: 16 vertices, 23 undirected edges");
        Console.WriteLine(path.Count == 0 ? "No path found." : string.Join(" -> ", path.Select(i => Names[i])));
        Console.WriteLine($"Total cost: {cost}");
        Console.WriteLine($"Bee nodes on path: {path.Count(i => Bees.Contains(Names[i]))}");
    }

    static int?[,] CreateHorseGraph()
    {
        int?[,] graph = new int?[16, 16];
        void Link(int a, int b) => graph[a, b] = graph[b, a] =
            Bees.Contains(Names[a]) || Bees.Contains(Names[b]) ? 5 : 1;
        for (int row = 0; row < 3; row++)
        for (int col = 0; col < 5; col++)
        {
            int i = row * 5 + col;
            if (col < 4) Link(i, i + 1);
            if (row < 2) Link(i, i + 5);
        }
        Link(0, 15);
        return graph;
    }

    public static (List<int> Path, long Cost) ShortestPath(int?[,] graph, int start, int target)
    {
        int count = graph.GetLength(0);
        if (count != graph.GetLength(1)) throw new ArgumentException("Matrix must be square.");
        if (start < 0 || start >= count || target < 0 || target >= count)
            throw new ArgumentOutOfRangeException(nameof(start));
        foreach (int? weight in graph)
            if (weight < 0) throw new ArgumentException("Negative weights are not supported.");

        long[] distance = Enumerable.Repeat(long.MaxValue, count).ToArray();
        int[] parent = Enumerable.Repeat(-1, count).ToArray();
        bool[] settled = new bool[count];
        distance[start] = 0;
        for (int step = 0; step < count; step++)
        {
            int current = -1;
            for (int i = 0; i < count; i++)
                if (!settled[i] && distance[i] != long.MaxValue &&
                    (current == -1 || distance[i] < distance[current])) current = i;
            if (current == -1) break;
            settled[current] = true;
            if (current == target) break;
            for (int next = 0; next < count; next++)
            {
                if (settled[next] || graph[current, next] is not int weight) continue;
                long candidate = checked(distance[current] + weight);
                if (candidate >= distance[next]) continue;
                distance[next] = candidate;
                parent[next] = current;
            }
        }
        List<int> path = new();
        if (distance[target] == long.MaxValue) return (path, long.MaxValue);
        for (int i = target; i != -1; i = parent[i]) path.Add(i);
        path.Reverse();
        return (path, distance[target]);
    }

    static void RunChecks(int?[,] horse)
    {
        int passed = 0;
        void Check(bool condition, string name)
        {
            if (!condition) throw new Exception("FAIL: " + name);
            Console.WriteLine("PASS: " + name); passed++;
        }
        Check(horse.Cast<int?>().Count(w => w.HasValue) == 46, "23 undirected edges");
        var result = ShortestPath(horse, 9, 15);
        Check(result.Cost == 10 && string.Join(",", result.Path.Select(i => Names[i])) ==
            "Start,A,C,F,G,H,K,N,M,L,End", "Expected horse route and cost");
        Check(result.Path.All(i => !Bees.Contains(Names[i])), "No bees");
        Check(ShortestPath(new int?[2, 2], 0, 1).Path.Count == 0, "Unreachable target");
        Check(ShortestPath(new int?[1, 1], 0, 0).Cost == 0, "Start equals target");
        int?[,] zero = { { null, 0 }, { 0, null } };
        Check(ShortestPath(zero, 0, 1).Cost == 0, "Zero-weight edge");
        int?[,] large = { { null, int.MaxValue, null }, { int.MaxValue, null, int.MaxValue }, { null, int.MaxValue, null } };
        Check(ShortestPath(large, 0, 2).Cost == 2L * int.MaxValue, "Large distance");
        bool rejected = false;
        try { ShortestPath(new int?[,] { { null, -1 }, { -1, null } }, 0, 1); }
        catch (ArgumentException) { rejected = true; }
        Check(rejected, "Reject negative edge");
        Console.WriteLine($"All {passed} checks passed.");
    }
}
