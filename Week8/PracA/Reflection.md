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
