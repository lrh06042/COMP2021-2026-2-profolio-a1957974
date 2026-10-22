# Task 2 — AI Code Generation

## Implementation and reproduction
The independent project is `AIComparison/HorseDijkstraAI`. It has its own project file and does not reference Task 1.

From the PracB directory:
```powershell
dotnet run --project AIComparison/HorseDijkstraAI/HorseDijkstraAI.csproj
dotnet run --project AIComparison/HorseDijkstraAI/HorseDijkstraAI.csproj -- --test
```
Alternatively, enter that project directory and run `dotnet run`.

Expected result: Start -> A -> C -> F -> G -> H -> K -> N -> M -> L -> End. Total cost: 10. Bee nodes: 0.

## 1. What AI tool did you use?
OpenAI Codex was used in this chat to generate and verify the second implementation. Task 1 was also developed with AI assistance, so this is a comparison between the Task 1 baseline and an alternative AI-generated implementation, not a claim that Task 1 was entirely written without AI.

## 2. What were some prompts you used?
The actual user requests were “现在开始完成task2” (now start completing Task 2), followed by “完成所有parcb内容我要提交作业了” (complete all PracB content; I am preparing to submit). The earlier workshop screenshots and Task 1 implementation provided the context. No separate detailed English prompt was submitted to another AI tool. Codex chose the matrix-based approach within this conversation.

## 3. Did the AI solve it in one attempt, or require revision?
The alternative implementation passed its first build and execution, with zero compiler warnings and zero errors. Eight checks passed: graph edge count, expected route and cost, avoiding bees, unreachable target, identical start and target, zero-weight edges, large distances and rejecting negative weights. No code revision was needed after the first generated version. These checks provide evidence for the tested cases, rather than proof of correctness for every possible input.

## 4. What was substantially different?
Task 1 represents the graph using Node objects and Dictionary<Node, int> adjacency lists, then chooses the next node with a PriorityQueue. The alternative uses integer vertex indexes, an int?[,] adjacency matrix, arrays for distances and parents, and a settled array. It scans all vertices to find the unsettled vertex with the smallest distance. A null matrix entry means no edge, so zero remains a valid edge weight.

## 5. What did the alternative do better?
The alternative has straightforward queue-free state management: each iteration settles one vertex, and there are no duplicate queue entries or stale priorities to explain. For this fixed 16-vertex graph, its arrays and deterministic lowest-index tie handling make it easy to trace manually. It also validates that the matrix is square and that the start and target indexes are in range. These are advantages of its representation, not a claim that it is generally faster.

## 6. What did the alternative do worse?
It takes O(V²) time and O(V²) graph storage because it scans vertices and stores every possible pair, even for sparse graphs. Task 1's heap and adjacency-list approach is normally O((V + E) log V) on simple graphs and stores the graph in O(V + E) space. Indexes such as 9 and 15 are also less expressive than named Node references, and the alternative's code is concentrated in one file. The Task 1 design therefore scales better to large sparse graphs and separates responsibilities more clearly.

## Attachments
`Attachments/Task2-AI/Program.cs.txt` is the generated C# source, copied as text to avoid accidental compilation in the main project. The runnable copy remains in `AIComparison/HorseDijkstraAI/Program.cs`. The project file and SDK configuration are also supplied with the attachments. `Task2-output.txt` and `Task2-tests.txt` record the actual run and checks.
