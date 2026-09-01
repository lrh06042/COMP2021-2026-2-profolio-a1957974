Task 1
In Task 1, I created a string array containing ten names and used a `foreach` loop to print every value. I compared the length of each name to find the longest and shortest names.

I used the `Array.Sort()` method to sort the array. By default, C# sorts strings in ascending lexicographical order, which is similar to alphabetical order. It compares the characters in each string from left to right. I then used `Array.Reverse()` to reverse the order of the sorted array.
Task 2: Using Lists

In Task 2, I created a `List<string>` to manage student names. I used `Add()`, `Remove()`, `Insert()` and `AddRange()` to change the contents of the list. I used `IndexOf()` to find the index of a complete name and `Contains()` to find names containing part of a string. I also used a loop to calculate the total length of all names. Finally, I converted the Task 1 array into a list and converted the completed list back into an array.

Task 3: Comparing Arrays and Lists

Arrays use syntax such as `string[] names`, while lists use syntax such as `List<string> studentNames`. An array has a fixed size after it is created. A list can grow or shrink and provides methods such as `Add()`, `Remove()` and `Insert()`.

Both arrays and lists provide fast access to values using an index. Arrays normally use less memory and work well when the number of values is known. Lists are more flexible, but inserting or removing an item can be slower because other items may need to move.

I would use an array when I know the exact number of items and the size will not change. I would use a list when items need to be added or removed while the program is running.

