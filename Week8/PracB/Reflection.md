# Week 8 - PracB Reflection

## Task 1: Basic File I/O

### 1. What happens if the text file already exists when you write to it?

With the original `if (!File.Exists(path))` check, writing is skipped when the file already exists. In the initial experiment, existing text was preserved and displayed by StreamReader. After removing this check, `File.CreateText(path)` replaced the existing contents with `Hello World` and a newline. It did not append the new text, and no characters from the longer previous contents remained. Appending would require a different operation, such as File.AppendText.

### 2. What does the @ symbol before the quoted string mean?

The @ prefix creates a verbatim string literal. Backslashes are treated literally, so a Windows path can be written as `@"C:\temp\MyTest.txt"` instead of `"C:\\temp\\MyTest.txt"`. It does not create a directory or make the path portable. The final default path is `@"./MyTest.txt"`; the @ prefix is optional here because forward slashes do not need escaping.

### 3. Do you need to wrap file access with using StreamReader and using StreamWriter? What happens if you do not?

Streams need to be disposed reliably. A using statement automatically calls Dispose when its block ends, including when an exception is thrown. Disposing the StreamWriter flushes buffered text and closes the file before it is read. Without proper disposal, the file can remain open and buffered output may not be written completely. Using is not the only option: an explicit Dispose in a finally block can also work. File.ReadAllText manages opening and closing the file internally, so the returned string does not need a using block.

### 4. How is StreamReader different from File.ReadAllText()?

StreamReader can process a file incrementally, for example one line at a time with ReadLine, without keeping the entire file in one string. The initial implementation used this approach. The final implementation follows step 5 and uses File.ReadAllText(path), which reads the whole file into a string and closes it automatically. This is convenient for this small file but requires memory for the entire contents. ReadLine removes each line terminator, whereas ReadAllText retains the file's line breaks.

### Path and overwrite experiments

The experiments were run in a separate scratch directory before copying the final code into this project.

| Experiment | Observed result |
| --- | --- |
| Relative path `./MyTest.txt` | The file was created in the process's current working directory. |
| Existing file with the original File.Exists guard | Existing text was preserved. |
| Existing longer text after removing the guard | Contents became exactly `Hello World` followed by a newline. |
| Absolute path to an existing scratch directory | Writing and reading both succeeded at that location. |
| Relative path `./missing-folder/MyTest.txt` | DirectoryNotFoundException was caught and reported; the program returned exit code 1. The parent folder was not created automatically. |

The program prints both the working directory and the resolved file path, making the output file easy to find. A relative path is resolved against the working directory, which can vary between launch methods; it is not necessarily the source or executable directory.

### Running the final version

From the Week8/PracB directory:

```powershell
dotnet run
```

This creates MyTest.txt in the working directory. To write directly to the existing Attachments directory, run from Week8/PracB:

```powershell
dotnet run -- ../Attachments/MyTest.txt
```

The optional first command-line argument supplies the path for further experiments. Without an argument, the program uses `./MyTest.txt`.

### Output

- [MyTest.txt](../Attachments/MyTest.txt)
- Text read back: `Hello World`

### References

- [Microsoft: File.CreateText](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.createtext)
- [Microsoft: File.ReadAllText](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltext)

## Task 2: Serialise and Deserialise a List

### 1. What does the ! symbol at the end of the Deserialize line do?

The postfix ! is the null-forgiving operator. Deserialize can return null, so assigning its result to a non-nullable List<Player> can produce a compiler warning. Adding ! suppresses that warning for the expression; it does not change the returned value, check for null, or prevent an exception. The Task 2 version also checked loadedPlayers for null before using it. In the final refactored version, FileReader checks its deserialized result for null and does not need the ! operator.

### 2. What happens if the class properties are not public?

With the default System.Text.Json settings used here, non-public properties are not included in the JSON. The PracA version had a public Name property but non-public Health, Attack and Defence properties, so those statistics would not be saved with the default settings. The separate PracB Player class makes all four properties public with getters and setters, as required by the task. If a class has no eligible properties, its JSON representation can be an empty object, {}. Special attributes and serializer configuration can change this behaviour, but are not needed for this task.

### Implementation and validation

Five new Player objects were added to a List<Player>. JsonSerializer.Serialize converted the list to an indented JSON string, which was printed to the console and written to Players.json. In the Task 2 version, File.ReadAllText read the saved file into a separate loadedJson variable, and JsonSerializer.Deserialize<List<Player>> restored a new list. Task 3 moved these operations into FileReader<T>, which is used in the final code.

The program displayed the restored players and compared the count and all four properties against the original list. The run produced:

```text
Players loaded from disk: 5
Name: Alice the mighty!
Stats: HP=100, ATK=15, DEF=8
Name: Bob the mighty!
Stats: HP=120, ATK=12, DEF=10
Name: Charlie the mighty!
Stats: HP=90, ATK=20, DEF=5
Name: Diana the mighty!
Stats: HP=110, ATK=18, DEF=7
Name: Ethan the mighty!
Stats: HP=80, ATK=25, DEF=4
All player data matches: True
```

ToString formats the readable player output; JSON serialization uses the properties, not ToString.

### Running Tasks 1 and 2

From Week8/PracB, run:

```powershell
dotnet run -- ../Attachments/MyTest.txt
```

The program runs Task 1 followed by Task 2. Players.json is saved beside MyTest.txt, so this command places both files in Week8/Attachments. Running without an argument instead places both files in the current working directory.

### Output

- [Players.json](../Attachments/Players.json)

## Task 3: Refactor I/O methods

### 1. What does where T : class mean?

It is a generic type constraint requiring T to be a reference type. List<Player>, LinkedList<Player>, Player and string are examples of reference types. Value types such as int and structs do not satisfy this constraint. The constraint does not mean that T must be Player, nor does it guarantee that every allowed type can be serialized by System.Text.Json.

The same FileWriter<T> and FileReader<T> methods can therefore be reused with different supported reference types. In this task, T is List<Player>. The writer receives the list as objectToWrite, and the reader returns a List<Player> loaded from the specified file.

### Refactoring

Both methods use the exact signatures required by the task:

```csharp
public static void FileWriter<T>(string path, T objectToWrite) where T : class
public static T FileReader<T>(string path) where T : class
```

FileWriter serializes the supplied object, displays the JSON preview retained from Task 2, and writes it to disk. FileReader reads the file, deserializes it as T, and returns the restored object. It checks for a null result and throws JsonException instead of returning null. This replaces the null-forgiving operator used in the Task 2 version with an explicit runtime check.

Main now calls the methods as follows:

```csharp
FileWriter<List<Player>>(jsonPath, players);
List<Player> loadedPlayers = FileReader<List<Player>>(jsonPath);
```

The JSON reading, writing and serialization logic has moved out of Main. The separate plain-text demonstration from Task 1 remains in Main.

### Validation

The refactored program was run using the same command from Week8/PracB:

```powershell
dotnet run -- ../Attachments/MyTest.txt
```

It saved and loaded all five players. Comparing the count, Name, Health, Attack and Defence produced:

```text
Players loaded from disk: 5
All player data matches: True
```

### Output

- [FileWriter and FileReader implementation screenshot](../Screenshots/Topic8B-Task3.png)

## Task 4: Displaying memory usage for LinkedList

### 1. What is the purpose of the Garbage Collector?

The Garbage Collector (GC) automatically manages managed memory. It identifies objects that are no longer reachable from the program's live roots and reclaims their storage for reuse. It may also compact parts of the heap. Objects that remain reachable are not collected just because GC.Collect is called. Resources such as open files still need to be disposed reliably with using or Dispose.

### 2. Why does it know the current amount of bytes allocated to the Heap?

The .NET runtime manages allocations on the managed heap, and the GC tracks the heap's regions and objects to perform collection. It can therefore report heap statistics. More precisely, GC.GetGCMemoryInfo().HeapSizeBytes reports the total heap size at the last garbage collection, rather than an exact live counter for one collection or object. Calling GC.Collect before querying it obtains information from a recent collection.

### 3. What does GC.Collect() do?

The parameterless GC.Collect forces a garbage collection across all generations. Unreachable objects can have their storage reclaimed, while reachable objects remain. It does not clear every object, guarantee that memory is immediately returned to the operating system, or reset the process's historical peak memory. Manual collection is used here for the exercise; normal application code usually lets the runtime schedule collections.

### 4. Was PeakWorkingSet64 different from HeapSizeBytes? Why?

Yes. In the final Release verification run on .NET 10.0.12 from Week8/PracB, HeapSizeBytes was 404,136 bytes and PeakWorkingSet64 was 30,162,944 bytes. HeapSizeBytes measures the managed heap at the last GC. PeakWorkingSet64 measures the process's highest physical working set since it started, which can include runtime components, executable code, thread stacks and unmanaged memory as well as managed memory. They differ in both scope and measurement time, so they are not expected to match. Results can vary between runs.

### Implementation and observed output

The five existing Player objects were added to a LinkedList<Player> with AddLast. FileWriter<LinkedList<Player>> saved the linked list, and FileReader<LinkedList<Player>> loaded a new linked list from disk. The restored count and every Name, Health, Attack and Defence value were checked against the original players.

After GC.Collect, the program read HeapSizeBytes and Process.GetCurrentProcess().PeakWorkingSet64. GC.KeepAlive after the measurements ensures that the relevant collections remain alive through the measurement point. Tasks 1-3 run before this task and their collections are retained, so these figures describe the whole application, not the isolated cost of five linked-list nodes.

```text
Task 4 results: LinkedList<Player>
LinkedList players loaded from disk: 5
All LinkedList player data matches: True
HeapSizeBytes: 404,136 bytes
PeakWorkingSet64: 30,162,944 bytes
```

### Run and submission output

From Week8/PracB:

```powershell
dotnet run -- ../Attachments/MyTest.txt
```

- [Linked-list JSON file](../Attachments/LinkedPlayers.json)
- Required screenshot still to capture: the final Task 4 console results, including both memory values. Save it as Screenshots/Topic8B-Task4.png. Values in a new run may differ from the recorded run above.

### References

- [Microsoft: HeapSizeBytes](https://learn.microsoft.com/en-us/dotnet/api/system.gcmemoryinfo.heapsizebytes)
- [Microsoft: PeakWorkingSet64](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.peakworkingset64)

## Task 5: Benchmarking memory for List and LinkedList

### Experiment

Each collection type was tested in its own fresh process with the same Release build and .NET 10.0.12 runtime. The benchmark mode returns before Tasks 1-4, preventing their objects and earlier memory peaks from affecting this comparison. Both tests started with an empty collection and used a for loop from i = 0 to i < 10_000. Each iteration created a new Player with the name Player i, Health 100, Attack 10 and Defence 5. List used Add and LinkedList used AddLast.

The collection was saved using FileWriter<T>, then loaded from disk using FileReader<T>. The writer's console preview was temporarily suppressed during each benchmark to avoid printing 10,000 JSON objects; the writer's implementation was unchanged. Both loaded collections were checked for all 10,000 names, statistics and their order. No validation failures occurred.

GC.Collect was called before querying HeapSizeBytes and PeakWorkingSet64. The original and loaded collections were both kept alive until after the measurement using GC.KeepAlive in both cases. Therefore, each run retained two collections with 10,000 Player objects each, rather than measuring only one collection. This policy was the same for both data structures.

### Recorded results

These are actual measurements from separate executions of the submitted project:

| Collection | Players created | Players loaded | HeapSizeBytes (bytes) | PeakWorkingSet64 (bytes) |
| --- | ---: | ---: | ---: | ---: |
| List<Player> | 10,000 | 10,000 | 10,575,848 | 46,931,968 |
| LinkedList<Player> | 10,000 | 10,000 | 11,284,088 | 48,332,800 |

Both runs reported `All player data matches: True`.

### 1. Which class used more memory?

LinkedList<Player> had the higher value for both metrics in these runs. Its reported heap size was 708,240 bytes higher, and its peak working set was 1,400,832 bytes higher. These differences describe the measured program runs, not an exact per-node or per-collection memory size.

### 2. Why?

List<Player> stores references in an internal array. It can have unused capacity, but it does not need a separate node object for each entry. LinkedList<Player> allocates a node for every entry, with references for the value, neighbouring nodes and owning list, plus object overhead. This additional storage usually makes LinkedList use more memory for the same number of players. The Player objects themselves and their names are also part of both runs' memory usage.

The measured totals also include runtime and serializer data. HeapSizeBytes includes heap fragmentation and is a snapshot from the last GC; PeakWorkingSet64 includes the whole process's physical memory peak during creation, JSON writing, reading and validation. These single runs support the expected difference, but do not isolate the collection overhead precisely. Values may differ on later runs, and process peak measurements can be noisy.

### Reproducing the benchmark

Run these commands separately from Week8/PracB:

```powershell
dotnet run -c Release -- --benchmark list ../Attachments
dotnet run -c Release -- --benchmark linkedlist ../Attachments
```

Each command starts a fresh process. Do not combine the collection tests into one process when comparing PeakWorkingSet64, because that metric retains the peak from the process's earlier activity.

### Outputs

- [List JSON](../Attachments/Task5-List.json)
- [LinkedList JSON](../Attachments/Task5-LinkedList.json)
- [Recorded List console output](../Attachments/Task5-List-results.txt)
- [Recorded LinkedList console output](../Attachments/Task5-LinkedList-results.txt)
- Required screenshots still to capture: the two benchmark console outputs, with collection name, counts and both memory metrics visible. Suggested names: Screenshots/Topic8B-Task5-List.png and Screenshots/Topic8B-Task5-LinkedList.png. The text records above supplement, but do not replace, the requested screenshots.

Task 6 is optional and is not included in this submission.
