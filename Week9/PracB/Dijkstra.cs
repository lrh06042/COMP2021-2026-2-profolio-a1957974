namespace PARCB;

public static class Dijkstra
{
    public static (List<Node> Path, long Distance) FindPath(Graph graph, Node start, Node target)
    {
        if (!graph.Nodes.Contains(start) || !graph.Nodes.Contains(target))
            throw new ArgumentException("Start and target must belong to the graph.");
        if (graph.Nodes.Any(n => n.Edges.Values.Any(w => w < 0)))
            throw new ArgumentException("Dijkstra requires non-negative weights.");

        // long prevents overflow when adding several int edge weights.
        var DistanceList = graph.Nodes.ToDictionary(n => n, _ => long.MaxValue);
        DistanceList[start] = 0;
        PriorityQueue<Node, long> priorityQueue = new();
        Dictionary<Node, Node?> parents = new() { [start] = null };
        priorityQueue.Enqueue(start, 0);

        while (priorityQueue.TryDequeue(out Node? current, out long queuedDistance))
        {
            // Skip old queue entries superseded by a cheaper route.
            if (queuedDistance != DistanceList[current]) continue;
            if (current == target) break;
            foreach (var neighbour in current.Edges)
            {
                long candidate = checked(DistanceList[current] + neighbour.Value);
                if (candidate >= DistanceList[neighbour.Key]) continue;
                DistanceList[neighbour.Key] = candidate;
                parents[neighbour.Key] = current;
                priorityQueue.Enqueue(neighbour.Key, candidate);
            }
        }

        List<Node> path = new();
        if (DistanceList[target] == long.MaxValue)
            return (path, long.MaxValue);
        for (Node? node = target; node != null; node = parents[node])
            path.Add(node);
        path.Reverse();
        return (path, DistanceList[target]);
    }
}
