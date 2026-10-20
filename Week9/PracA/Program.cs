namespace PracA;

internal class Program
{
    static void Main(string[] args)
    {
        Graph graph = new Graph();

        Node A = new Node("A");
        Node B = new Node("B");
        Node C = new Node("C");
        Node D = new Node("D");
        Node E = new Node("E");
        Node F = new Node("F");
        Node G = new Node("G");
        Node H = new Node("H");
        Node I = new Node("I");

        graph.AddEdge(A, B);
        graph.AddEdge(A, C);
        graph.AddEdge(B, D);
        graph.AddEdge(B, E);
        graph.AddEdge(C, F);
        graph.AddEdge(D, G);
        graph.AddEdge(E, H);
        graph.AddEdge(F, I);

        Console.WriteLine("Task 1: Building a Graph");
        Console.WriteLine("Node: adjacent nodes");
        Console.WriteLine(graph.ToString());
    }
}
