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
