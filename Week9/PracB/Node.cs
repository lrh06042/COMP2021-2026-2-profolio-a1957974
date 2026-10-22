namespace PARCB;

public class Node
{
    public string Data { get; set; }
    public Dictionary<Node, int> Edges { get; } = new();

    public Node(string data) => Data = data;
}
