namespace PARCB;

public static class HorseGraph
{
    // Reconstructed from the supplied diagram: no diagonal movement.
    public static (Graph Graph, Node Start, Node End) Create()
    {
        string[,] layout =
        {
            { "L", "I", "F", "C", "A" },
            { "M", "J", "G", "D", "Start" },
            { "N", "K", "H", "E", "B" }
        };
        HashSet<string> bees = new() { "I", "J", "D", "E", "B" };
        var nodes = new Dictionary<string, Node>();
        foreach (string name in layout)
            nodes.Add(name, new Node(bees.Contains(name) ? $"{name} (BEES)" : name));
        nodes.Add("End", new Node("End"));

        Graph graph = new();
        void Connect(string a, string b) =>
            graph.AddEdge(nodes[a], nodes[b], bees.Contains(a) || bees.Contains(b) ? 5 : 1);

        for (int row = 0; row < 3; row++)
        for (int col = 0; col < 5; col++)
        {
            if (col < 4) Connect(layout[row, col], layout[row, col + 1]);
            if (row < 2) Connect(layout[row, col], layout[row + 1, col]);
        }
        Connect("L", "End");
        return (graph, nodes["Start"], nodes["End"]);
    }
}
