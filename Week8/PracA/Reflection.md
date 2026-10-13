# Week 8 - PracA Reflection

## Task 1: Using Hashtable

### 1. What happened when you tried to add the (Key: String, Value: int) entry? Why?

The entry with the string key "TestPlayer" and integer value 50 was added successfully. Hashtable stores keys and values as objects, so it does not enforce my intended Player-to-int types at compile time. I removed this test entry before sorting so that every remaining key could safely be cast to Player. A Dictionary<Player, int> would reject a string key at compile time.

### 2. What happened when you tried to add the duplicate key?

Adding the same alice object again with a score of 999 threw an ArgumentException. I caught the exception so the program could continue. The original score of 120 remained unchanged. The supplied Player class does not override Equals or GetHashCode, so two separate Player objects with the same name would not be considered the same key. This test deliberately reused the same object.

### 3. Were you able to use LINQ methods when writing your sorting algorithm? Why / why not?

Yes, after converting the entries with Cast<DictionaryEntry>(). Hashtable implements the non-generic IEnumerable interface, so I could not directly use the generic OrderByDescending method on it. Cast<DictionaryEntry>() provided an IEnumerable<DictionaryEntry>, allowing me to sort by each entry's value after casting it to int. The sorted sequence was printed in descending score order; this did not change the Hashtable's internal order.

### Observed console output

```text
String key added successfully: 50
Duplicate key rejected: ArgumentException.

Player scores (descending):
Diana: 300
Bob: 250
Ethan: 210
Charlie: 180
Alice: 120
```

## Task 2: Complexity analysis through benchmarking

### 1. Which approach was faster? Append or Build?

Build (StringBuilder) was faster at all three loop sizes in this run. At 100,000 characters, Append (string +=) took 1044.4314 ms, while Build took 0.1638 ms. Both methods produced identical strings of the expected length.

| Characters | Append (+=), ms | Build, ms | Outputs match |
| ---: | ---: | ---: | :---: |
| 10,000 | 43.4828 | 0.1161 | True |
| 50,000 | 212.9921 | 0.0970 | True |
| 100,000 | 1044.4314 | 0.1638 | True |

### 2. Why?

Strings are immutable. Each += operation creates a new string and copies the existing characters along with the new character. As the string grows, this repeated copying requires more work and generates temporary allocations that increase garbage collection pressure.

StringBuilder uses expandable storage and does not copy the entire accumulated text on every append. This makes it much faster for repeated concatenation in this experiment.

### Measurement notes

I used Stopwatch to time only the loops, with a fresh output object and timer for each method and each size. Both methods appended the same text, "z", the same number of times. Validation, StringBuilder.ToString(), and console output occurred after timing. All times use TotalMilliseconds with four decimal places.

These are individual measurements from a Release run, without a separate warm-up. JIT compilation, garbage collection, and system activity can affect the results. In particular, StringBuilder's very short timings are noisy: the 50,000-character measurement was slightly lower than the 10,000-character measurement. The precise values will vary between runs; these measurements show a clear performance difference, rather than proving a complexity curve.

The submitted console screenshot is in ../Screenshots/Topic8A-Task2.png. It records a separate run, so its timings differ slightly from the measurements above.

## Task 3: Graphing the analysis

### Experiment

I started with 20 passes, which took approximately 14 seconds, then increased iterations to 40. The final benchmark took 145.77 seconds (about 2 minutes 26 seconds). Each pass appended "z" n times, where n = pass * 10,000, giving input sizes from 10,000 to 400,000 characters.

Each method started with an empty output for each pass. Stopwatch measured the loops independently, and both lists stored TotalMilliseconds. Equality and length checks ran after timing; all 40 comparisons passed. Plotting, conversion of the builder to a string, and console output were excluded from each measured loop.

The two plots use the actual character count on the horizontal axis and milliseconds on the vertical axis. Their vertical scales differ because the runtimes differ greatly. The original measured points are shown without smoothing.

| Characters | Append (ms) | Build (ms) |
| ---: | ---: | ---: |
| 100,000 | 418.5290 | 0.0604 |
| 200,000 | 2368.6784 | 0.1239 |
| 300,000 | 5998.8033 | 0.1753 |
| 400,000 | 11232.1155 | 0.3197 |

### 1. What is the Big-O complexity of the Append method?

Repeated string concatenation in this loop has O(n^2) time complexity. Strings are immutable, so each iteration copies the growing string into a new string. Across n appends, the amount of copying is proportional to 1 + 2 + ... + n = n(n + 1) / 2, which grows quadratically.

### 2. How does your evidence support this conclusion?

appendPlot.png shows an upward-curving trend: the cost of adding another fixed block of characters generally increases as the input grows. From 200,000 to 400,000 characters, the measured time rose from 2368.6784 ms to 11232.1155 ms, about 4.74 times for twice the input. This is consistent with quadratic growth, rather than linear growth. It is not exactly four times because real timings also include allocation, garbage collection, and system effects. The graph supports the copying analysis but does not prove the complexity by itself.

### 3. What is the Big-O complexity of the Builder method?

Building the string with StringBuilder has O(n) overall time complexity for n fixed-size appends. A single append has amortized O(1) cost, so n appends require O(n) work. Occasional storage allocation does not change this overall classification. Calling ToString() would also require O(n) work, although that operation was outside the timed loop here.

### 4. How does your evidence support this conclusion?

buildPlot.png shows a generally increasing trend with some spikes. From 100,000 to 200,000 characters, time increased from 0.0604 ms to 0.1239 ms, about 2.05 times for twice the input. At 300,000 characters it was 0.1753 ms, about 2.90 times the 100,000-character result. These observations support approximately linear growth.

The measurements are very short and include noise: the 400,000-character result was 0.3197 ms, and some smaller inputs had higher times than later inputs. JIT compilation, allocation, scheduling, and garbage collection can affect individual points. These single measurements are evidence of the broad trend, not an exact timing law or a rigorous proof. Repeated measurements and warm-up would improve confidence.

### Output files

- [Append plot](../Attachments/appendPlot.png)
- [Build plot](../Attachments/buildPlot.png)
- [All 40 measured rows](../Attachments/Task3-timings.csv)
