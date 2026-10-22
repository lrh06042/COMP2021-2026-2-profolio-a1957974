namespace PARCB;

public static class GraphSearch
{
    public static (List<Node> Checked, List<Node> Path) DepthFirstSearch(Node start, Node target)
    {
        List<Node> checkedNodes = new List<Node>();
        HashSet<Node> visited = new HashSet<Node>();
        Stack<(Node Node, Node? Parent)> stacked = new Stack<(Node, Node?)>();
        Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
        bool found = false;
        stacked.Push((start, null));

        while (stacked.Count > 0)
        {
            var (current, parent) = stacked.Pop();
            // Cycles can create duplicate pending entries; process each node once.
            if (!visited.Add(current)) continue;
            if (parent != null) parents[current] = parent;
            checkedNodes.Add(current);
            if (current == target)
            {
                found = true;
                break;
            }

            foreach (Node neighbour in current.Edges.Keys)
            {
                if (visited.Contains(neighbour)) continue;
                // Commit this candidate parent only when its entry is visited.
                stacked.Push((neighbour, current));
            }
        }

        List<Node> path = new List<Node>();
        if (found)
        {
            Node current = target;
            path.Add(current);
            while (parents.TryGetValue(current, out Node? parent))
            {
                current = parent;
                path.Add(current);
            }
            path.Reverse();
        }
        return (checkedNodes, path);
    }

    public static (List<Node> Checked, List<Node> Path) BreadthFirstSearch(Node start, Node target)
    {
        List<Node> checkedNodes = new List<Node>();
        HashSet<Node> discovered = new HashSet<Node> { start };
        Queue<Node> queued = new Queue<Node>();
        Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
        bool found = false;
        queued.Enqueue(start);

        while (queued.Count > 0)
        {
            Node current = queued.Dequeue();
            checkedNodes.Add(current);
            if (current == target)
            {
                found = true;
                break;
            }

            foreach (Node neighbour in current.Edges.Keys)
            {
                // Mark on enqueue to prevent duplicate queue entries in cycles.
                if (!discovered.Add(neighbour)) continue;
                parents[neighbour] = current;
                queued.Enqueue(neighbour);
            }
        }

        List<Node> path = new List<Node>();
        if (found)
        {
            Node current = target;
            path.Add(current);
            while (parents.TryGetValue(current, out Node? parent))
            {
                current = parent;
                path.Add(current);
            }
            path.Reverse();
        }
        return (checkedNodes, path);
    }
}
