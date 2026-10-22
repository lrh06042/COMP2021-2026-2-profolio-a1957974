using PARCB;

if (args.Contains("--test")) { Task1Checks.Run(); return; }
var (graph, start, target) = HorseGraph.Create();
Console.WriteLine("Week 9 Prac B - Task 1: Dijkstra's Algorithm");
Console.WriteLine($"Graph: {graph.Nodes.Count} nodes, {graph.Nodes.Sum(n => n.Edges.Count) / 2} undirected edges");
var result = Dijkstra.FindPath(graph, start, target);
Console.WriteLine("\nDijkstra:");
Console.WriteLine(result.Path.Count == 0 ? "No path found." : string.Join(" -> ", result.Path.Select(n => n.Data)));
if (result.Path.Count > 0)
{
    Console.WriteLine($"Total cost: {result.Distance}");
    Console.WriteLine($"Bee nodes on path: {result.Path.Count(n => n.Data.Contains("(BEES)"))}");
}
PrintComparison("BFS", GraphSearch.BreadthFirstSearch(start, target).Path);
PrintComparison("DFS", GraphSearch.DepthFirstSearch(start, target).Path);

static void PrintComparison(string name, List<Node> path)
{
    Console.WriteLine($"\n{name} (ignores weights):");
    if (path.Count == 0) { Console.WriteLine("No path found."); return; }
    Console.WriteLine(string.Join(" -> ", path.Select(n => n.Data)));
    long cost = 0;
    for (int i = 1; i < path.Count; i++) cost += path[i - 1].Edges[path[i]];
    Console.WriteLine($"Edges: {path.Count - 1}; Total cost: {cost}");
}
