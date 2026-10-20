# Week 9 - PracA Reflection

## Task 1: Building a graph

### 1. What sort of graph have we created? Is it directed or undirected? Cyclic or acyclic? Weighted or unweighted?

The graph is undirected because AddEdge adds each node to the other node's Edges collection. It is acyclic because there is no closed path involving distinct edges. Storing both directions of one undirected edge does not create a cycle. The graph is also connected, so it is a tree. It is unweighted because no weights or costs are assigned to its edges.

### 2. Other than a binary search tree and a linked list, what are two commonly used data structures that are also graphs?

A binary heap and a trie are both graphs. A binary heap has the structure of a complete binary tree, even when implemented using an array. A trie is a tree that stores keys along paths, such as a sequence of letters in a word. Both structures consist of nodes connected by edges.

### Console output

```text
Task 1: Building a Graph
Node: adjacent nodes
A: B, C
B: A, D, E
C: A, F
D: B, G
E: B, H
F: C, I
G: D
H: E
I: F
```

### Required screenshots

- Node.cs class code: save as ../Screenshots/Topic9A-Task1-Node.png.
- Console showing Graph.ToString(): save as ../Screenshots/Topic9A-Task1-Output.png.

These screenshots still need to be captured from the editor and console.

## Task 2: Traversing with BFS

### 1. Why do we need to keep track of which nodes we have visited?

Undirected edges can lead back to the previous node, and cycles can lead around the same nodes repeatedly. Tracking discovered nodes prevents repeated work and infinite traversal. My BFS marks nodes when they enter the queue, so even a node that is waiting to be checked cannot be queued twice. A separate Checked list records the processing order, including the target I.

### 2. In the adjusted graph, which path does BFS take to I? Why?

BFS takes A -> B -> E -> H -> I, which has four edges. The other simple path, A -> B -> D -> G -> H -> I, has five edges. The FIFO queue processes nodes layer by layer, so H is discovered through E before the longer route through G can reach it. The Parents dictionary records only the first discovery of each node, giving a shortest path in this unweighted graph. The adjusted graph removes F-I and adds G-H and H-I.

### 3. What is the computational complexity of BFS? What does |V| + |E| mean?

Using an adjacency list and average constant-time HashSet and Dictionary operations, BFS has worst-case time complexity O(|V| + |E|). |V| is the number of vertices (nodes), and |E| is the number of edges. Each reachable node is queued at most once and each adjacency entry is examined at most once. An undirected edge is stored twice, but that constant factor does not change the complexity. The queue, discovered set, checked list and parent dictionary require O(|V|) auxiliary space. Reconstructing the path also takes at most O(|V|) time.

### Console output

```text
Task 2: BFS - initial graph
Start: A, Target: I, Found: True
Checked (including target): A -> B -> C -> D -> E -> F -> G -> H -> I
Path: A -> C -> F -> I

Task 2: BFS - adjusted graph
Start: A, Target: I, Found: True
Checked (including target): A -> B -> C -> D -> E -> F -> G -> H -> I
Path: A -> B -> E -> H -> I
```

The optional Parents dictionary and path reconstruction are implemented. Each call uses fresh search collections, and List<Node> retains the neighbour insertion order for reproducible output.

### Required screenshots

- BFS code in GraphSearch.cs: save as ../Screenshots/Topic9A-Task2-Code.png (use multiple screenshots if needed).
- Console showing both initial and adjusted Checked/Path results: save as ../Screenshots/Topic9A-Task2-Output.png.

These screenshots still need to be captured from the editor and console.
