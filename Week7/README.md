 Week 7 - Prac B Reflection

#Task 1

1. What is the data structure backing SortedDictionary? How did you find out?

SortedDictionary uses a balanced binary search tree. Microsoft's documentation describes it as a binary search tree, and the .NET source shows that its internal TreeSet uses the red-black tree implementation in SortedSet. These sources explain why the collection can keep its keys sorted as entries are added or removed.

Sources: [SortedDictionary documentation](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sorteddictionary-2), [SortedDictionary source](https://github.com/dotnet/runtime/blob/main/src/libraries/System.Collections/src/System/Collections/Generic/SortedDictionary.cs), [SortedSet source](https://source.dot.net/System.Collections/System/Collections/Generic/SortedSet.cs.html).

2. What happened when you tried to add a duplicate score? Why?

Adding Kevin with a score of 850 caused an ArgumentException because Alice already had that score. The score is the dictionary key, and keys must be unique. Adding Alice again with a score of 1000 worked because the name is the value, and values can repeat.

Source: [SortedDictionary.Add](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sorteddictionary-2.add).

3. Contrast SortedSet and SortedDictionary. Give an example of each.

SortedSet stores unique elements in sorted order. I could use it to store the stage numbers a player has unlocked. SortedDictionary stores key-value pairs ordered by key. I used it to connect scores to player names. Its keys must be unique, but its values do not have to be. The comparer determines which elements or keys count as equal.

Task 2

Reflection on the custom ranking system

At first, the program could not compare the ScoreEntry objects, so adding them to the dictionary caused an IComparable error. I added a ScoreComparer that compares the score first and then the stage reached. After that, all ten entries were added successfully, and removing Diana left nine entries.

The equal-score test made the comparison rule clearer. Kevin, Julia and Alice all had 850 points, but their stages were 9, 7 and 5, so they appeared in that order. Using the same Alice object with a new score of 1100 also worked. This showed me that dictionary values can repeat, while keys must be different according to the comparer. Two entries with both the same score and the same stage would still count as duplicate keys, even if their times or player names differed.

Task 3

1. What data structure backs a PriorityQueue? How did you find out?

.NET's PriorityQueue uses an array-backed quaternary min-heap. Each node can have up to four children, and the smallest priority value is processed first. Microsoft states this in the Remarks section of its documentation.

Source: [PriorityQueue documentation](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2).

2. What happens when two different players have the same priority?

Bob and Diana both had priority 1. Both were added successfully and processed before Charlie and Alice, whose priorities were 2 and 3. Bob came out first in this run, but the queue does not guarantee insertion order when priorities are equal.

Source: [PriorityQueue ordering guarantees](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2).

3. Why does PriorityQueue permit duplicates when SortedSet does not?

A priority queue keeps separate entries for processing, so several entries can share the same priority. A SortedSet stores unique elements and rejects elements that its comparer considers equal. Different players with equal priorities only count as duplicates in a SortedSet if its comparer treats them as equal, such as when it compares priority alone.

Sources: [PriorityQueue documentation](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2), [SortedSet source](https://source.dot.net/System.Collections/System/Collections/Generic/SortedSet.cs.html).

Task 4

1. Why was no custom IComparer or IComparable required for the Priority enum?

An enum already supports comparison using its underlying numeric values. HighPriority is 0, MediumPriority is 1, and LowPriority is 2. The default comparer can compare these values, so high-priority players come out first. The order comes from the numbers, rather than the names of the enum members.

Source: [Enum.CompareTo](https://learn.microsoft.com/en-us/dotnet/api/system.enum.compareto).

2. Would a struct containing an int Priority property require IComparer or IComparable?

Yes. Having an int property does not automatically make a custom struct comparable. The default comparer does not guess which property it should use. I would implement IComparable for the struct or pass an IComparer to the queue, with comparison code that compares the Priority properties. Without either option, an error occurs when comparison is needed.

Source: [Comparer<T>.Default](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.comparer-1.default).

