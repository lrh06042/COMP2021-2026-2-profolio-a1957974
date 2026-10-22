namespace PARCB;

public class Graph
{
    public List<Node> Nodes { get; } = new();

    public void AddEdge(Node fromNode, Node toNode, int weight)
    {
        if (weight < 0)
            throw new ArgumentOutOfRangeException(nameof(weight), "Dijkstra requires non-negative weights.");

        if (!Nodes.Contains(fromNode)) Nodes.Add(fromNode);
        if (!Nodes.Contains(toNode)) Nodes.Add(toNode);
        fromNode.Edges.Add(toNode, weight);
        toNode.Edges.Add(fromNode, weight);
    }
}
