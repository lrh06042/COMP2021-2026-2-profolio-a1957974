namespace PracA;

public static class GraphSearch
{
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

            foreach (Node neighbour in current.Edges)
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
