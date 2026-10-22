# Week 9 — Prac B

## Task 1: Dijkstra's Algorithm (70 minutes)
Implemented in Dijkstra.cs, called by Program.cs. The graph has 16 vertices and 23 undirected edges. Node.cs stores weighted neighbours; Graph.cs adds symmetric edges. GraphSearch.cs adapts the previous BFS/DFS implementation.

Run from this folder:
```powershell
dotnet run
dotnet run -- --test
```
Result: Start -> A -> C -> F -> G -> H -> K -> N -> M -> L -> End. Cost: 10. Bee nodes: 0.
BFS cost: 14. DFS cost: 50. All 12 checks passed.

Task 1 reflections: TASK1.md. Algorithm screenshot: Screenshots/Task1-Dijkstra.png.
Actual saved output: Task1-output.txt. Check results: Task1-tests.txt.

## Task 2: AI Code Generation (25 minutes)
Independent matrix-based Dijkstra project: AIComparison/HorseDijkstraAI.
```powershell
dotnet run --project AIComparison/HorseDijkstraAI/HorseDijkstraAI.csproj
dotnet run --project AIComparison/HorseDijkstraAI/HorseDijkstraAI.csproj -- --test
```
Same optimal route and cost. All 8 checks passed. Source copies are in Attachments/Task2-AI as .txt files. Six comparison reflections and actual prompting history: TASK2.md.

## Task 3: Wrapping up (25 minutes)
TASK3-DRAFT.md contains all seven suggested English answers. These personal statements require student confirmation or edits before submission.

## Submission checklist
- [x] Task 1 runnable implementation and BFS/DFS comparison.
- [x] Task 1 two reflection answers.
- [x] Algorithm code screenshot.
- [ ] Actual console screenshot showing the complete path and total cost.
- [x] Task 2 independently runnable AI implementation and source attachments.
- [x] Task 2 six comparison answers, truthful prompt and revision record.
- [ ] Task 3 seven personal answers confirmed by the student.

To finish screenshot evidence: run dotnet run in this folder and save a screenshot of the actual terminal under Screenshots/Task1-Console.png. The saved .txt log is supporting evidence, not a replacement for that screenshot.

AI assistance was used for both implementations and reflection drafts. Do not describe Task 1 as entirely unaided work. This package is prepared locally; it has not been uploaded or submitted.

The existing project file is PARCB.csproj and workspace file is PARCB.code-workspace; the folder is PracB. Only one .csproj is present at the root, so dotnet run resolves it automatically. Both projects use .NET 10 with no external NuGet package dependencies.
