namespace PracA;

public class Graph
{
    public List<Node> Nodes { get; }

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public void AddEdge(Node fromNode, Node toNode)
    {
        if (!Nodes.Contains(fromNode))
            Nodes.Add(fromNode);
        if (!Nodes.Contains(toNode))
            Nodes.Add(toNode);

        // An undirected edge connects the two nodes in both directions.
        if (!fromNode.Edges.Contains(toNode))
            fromNode.Edges.Add(toNode);
        if (!toNode.Edges.Contains(fromNode))
            toNode.Edges.Add(fromNode);
    }

    public override string ToString()
    {
        List<string> lines = new List<string>();
        foreach (Node node in Nodes)
        {
            string neighbours = string.Join(", ", node.Edges.Select(edge => edge.Data));
            lines.Add($"{node.Data}: {neighbours}");
        }
        return string.Join(Environment.NewLine, lines);
    }
}
