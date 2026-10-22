# Task 1 — Dijkstra's Algorithm

## Run
在本目录运行 `dotnet run`；运行 `dotnet run -- --test` 检查边界情况。

Dijkstra.cs 实现最短路径；Program.cs 输出结果并对比 BFS/DFS。GraphSearch.cs 从 PracA 迁移，邻居遍历改为 Edges.Keys。PracA 未修改。

## Reflection 1 — How does the implementation work?
The start node is assigned distance zero, while all other nodes begin at infinity. A priority queue selects the node with the smallest known total distance. For each neighbour, the algorithm adds the edge weight to the current distance. If this is cheaper than the recorded distance, it updates the distance and parent and enqueues the neighbour with the new priority. Old queue entries are skipped when their priority no longer matches the best distance. Because weights are non-negative, the target's shortest distance is final when its current best entry is removed from the queue. The program then follows parents from the target to the start and reverses that list to print the route. An unreachable target returns an empty path.

## Reflection 2 — Complexity compared with BFS/DFS
For a simple graph stored as adjacency lists, heap-based Dijkstra is commonly expressed as O((V + E) log V). This implementation allows duplicate queue entries instead of decreasing priorities in place. There are at most O(E) successful relaxations and queued entries, giving O(V + E log(E + 1)) time, within the usual bound for simple graphs, and O(V + E) auxiliary space. Skipping stale entries avoids scanning a node's neighbours again for an outdated route.

BFS and DFS take O(V + E) time: they process each node and edge a constant number of times using O(1) queue or stack operations. Dijkstra adds heap operations to select the lowest accumulated cost. BFS finds the fewest-edge route in an unweighted or equal-weight graph. DFS generally does not guarantee a shortest path. Neither minimises arbitrary weighted costs.

## Actual horse graph results
- Dijkstra: Start -> A -> C -> F -> G -> H -> K -> N -> M -> L -> End; cost 10; no bees.
- BFS: Start -> A -> C -> F -> I (BEES) -> L -> End; cost 14; 6 edges.
- DFS: cost 50; 14 edges. Traversal routes depend on neighbour insertion order.

## Implementation notes
Edge weights remain int. Total distances and queue priorities use long to avoid int overflow when adding large edge weights; infinity is long.MaxValue. The method is extracted from Program.cs so it can be checked with other graphs. The graph was reconstructed from the visible course diagram because the Graph setup section was collapsed.

## Submission evidence
Task1-output.txt and Task1-tests.txt are saved outputs from actual runs. The course additionally asks for screenshots of the algorithm and console path output.

This code and reflection draft were prepared with AI assistance. Read and understand them, and acknowledge that assistance accurately in the Task 2 comparison.
