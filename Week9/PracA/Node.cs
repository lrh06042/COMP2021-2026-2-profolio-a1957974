namespace PracA;

public class Node
{
    public string Data { get; set; }
    public List<Node> Edges { get; }

    public Node(string data)
    {
        Data = data;
        Edges = new List<Node>();
    }
}
