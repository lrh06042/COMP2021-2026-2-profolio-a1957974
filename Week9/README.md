# Week 9 - Prac A

- [Code](PracA)
- [Reflection: Tasks 1-3](PracA/Reflection.md)
- [Screenshots](Screenshots)

## Run

The project uses .NET 10, with no external packages. global.json selects a stable .NET 10 SDK.

From the repository root:

```powershell
cd .\Week9\PracA
dotnet run -c Release
```

Run one task at a time for screenshots:

```powershell
dotnet run -c Release -- task1
dotnet run -c Release -- task2
dotnet run -c Release -- task3
```

Task 3 also prints a BFS comparison using the same Task 3 adjusted graph.

## Submission checklist

| Requirement | Status |
| --- | --- |
| Task 1: Node, Graph, A-I and all eight original edges | Complete |
| Task 1: Graph.ToString lists nodes and neighbours | Complete |
| Task 2: Queue-based BFS, original and adjusted graphs | Complete |
| Task 3: Stack-based DFS, original and adjusted graphs | Complete |
| Optional parent dictionaries and path reconstruction | Complete for BFS and DFS |
| Reflection: all eight questions | Complete |
| Required code and console screenshots | Pending: see below |

Optional generic Node/Graph classes are not implemented; string data meets the required task. No Attachments folder is required.

## Screenshots to capture

Save actual editor and console screenshots into Screenshots:

| Filename | Required content |
| --- | --- |
| Topic9A-Task1-Node.png | Entire Node.cs class |
| Topic9A-Task1-Output.png | task1 console output, including all A-I neighbours |
| Topic9A-Task2-Code.png | Entire BreadthFirstSearch method in GraphSearch.cs |
| Topic9A-Task2-Output.png | task2 output for both original and adjusted graphs |
| Topic9A-Task3-Code.png | Entire DepthFirstSearch method in GraphSearch.cs |
| Topic9A-Task3-Output.png | task3 output for both original and adjusted graphs |

Use additional numbered code screenshots if a method does not fit legibly on screen. Keep the task titles and Checked/Path lines visible in console screenshots. Reflection.md contains the matching output text, but text is not a substitute for the required screenshots.

## Expected paths

| Search | Path | Nodes checked (including I) |
| --- | --- | ---: |
| BFS, original | A -> C -> F -> I | 9 |
| BFS, Task 2 adjusted | A -> B -> E -> H -> I | 9 |
| DFS, original | A -> C -> F -> I | 4 |
| DFS, Task 3 adjusted | A -> C -> F -> H -> I | 5 |
| BFS, Task 3 adjusted | A -> C -> F -> I | 9 |

The Task 2 adjusted graph removes F-I and adds G-H and H-I. The Task 3 adjusted graph keeps all original edges and adds D-E, E-F, F-H, G-H and H-I. Node order is determined by the insertion order of the adjacency lists.
