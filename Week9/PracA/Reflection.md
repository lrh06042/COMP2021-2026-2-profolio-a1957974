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
