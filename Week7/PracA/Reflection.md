Task 1 – Reflection
1. Did you need to run .Sort() to maintain the ordering? Why or why not?
I did not need to use .Sort(). When I printed the scores, they were already in ascending order. After I added 1100, it appeared at the end automatically. This showed that SortedSet maintains the order itself.
2. What is the underlying data structure used by SortedSet? How did you find out?
SortedSet uses a red-black tree, which is a self-balancing binary search tree. The .NET source code explains this in its comments. This helped me understand why the scores stayed sorted even when I added a new value.
3. What happened when you tried to add a duplicate score? Why does SortedSet behave this way?
When I tried to add 1100 again, the program displayed Duplicate added: False. The count stayed at 11, and the scores did not change. This happened because SortedSet only keeps unique values, so it did not add a second copy of the same score.