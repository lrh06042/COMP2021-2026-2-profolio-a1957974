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

The saved console output is in ../Screenshots/Topic8A-Task2-output.txt. A screenshot of the actual console output should be captured for submission.
