# Week 8

- [PracA](PracA/Reflection.md)
- [PracB reflections: Tasks 1-5](PracB/Reflection.md)
- [Screenshots](Screenshots)
- [Attachments](Attachments)

## Prac B setup

The project targets .NET 10. Install a .NET 10 SDK. The PracB/global.json file selects a stable .NET 10 SDK, allowing later .NET 10 feature bands, rather than automatically selecting a preview SDK. No external NuGet packages are needed.

From the repository root:

```powershell
cd .\Week8\PracB
```

## Run Tasks 1-4

```powershell
dotnet run -c Release -- ../Attachments/MyTest.txt
```

This writes and reads the text file, saves and restores five players using the generic JSON methods, and saves and restores a five-player LinkedList before displaying both memory metrics. It checks that all restored player data matches.

## Run Task 5

Run each command separately. Each benchmark starts a fresh process and skips Tasks 1-4.

```powershell
dotnet run -c Release -- --benchmark list ../Attachments
dotnet run -c Release -- --benchmark linkedlist ../Attachments
```

Each run creates 10,000 distinct Player objects, saves and loads the collection, validates every player, and prints HeapSizeBytes and PeakWorkingSet64. Both the original and restored collection remain alive through measurement. Results are whole-application metrics and can vary between runs.

## Prac B submission check

| Task | Required output | Status at the final code review |
| --- | --- | --- |
| 1 | Attachments/MyTest.txt | Generated and verified |
| 2 | Attachments/Players.json | Five players, all four properties verified |
| 3 | Screenshots/Topic8B-Task3.png | Present; method bodies still match the code |
| 4 | Console screenshot showing both memory metrics | Still needs to be captured |
| 5 | List and LinkedList console screenshots showing both metrics | Still need to be captured |

All Task 1-5 reflection questions are answered in PracB/Reflection.md. The Attachments folder also contains the five-player linked-list JSON, both 10,000-player JSON files and text records from the final verification runs. These text records are supporting evidence, not replacements for the required console screenshots.

Suggested screenshot names:

- Screenshots/Topic8B-Task4.png
- Screenshots/Topic8B-Task5-List.png
- Screenshots/Topic8B-Task5-LinkedList.png

Capture the Task 4 summary at the end of the Tasks 1-4 run, and each Task 5 summary after its corresponding command. Keep the task/collection name, player counts and both byte values visible. After saving the screenshots, update the pending screenshot entries in this checklist and Reflection.md.

Task 6 is optional and has not been implemented. Build folders bin and obj are excluded by the repository's .gitignore.
