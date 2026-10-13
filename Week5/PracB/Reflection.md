Week 5 Prac B

 Task 1 Reflection

How is the structure of a `LinkedList<T>` different from `List<T>`?

A `List<T>` stores its elements in a dynamic array, so an element can be accessed directly using an index. A `LinkedList<T>` stores each element inside a node. Each node contains a value and references to the previous and next nodes. This makes insertion and removal efficient when the node is already known, but accessing a particular position requires traversing the list.

Why can't a `LinkedList<string>` be initialized using standard List syntax?

The collection syntax needs a clear method for adding each element, normally an `Add()` method. `LinkedList<T>` does not have a general `Add()` method because an item could be added at the beginning or end. Instead, it provides methods such as `AddFirst()` and `AddLast()`.

A linked list can instead be created using its constructor:

```csharp
LinkedList<string> stringLinkedList =
    new LinkedList<string>(
        new[] { "Why", "isn't", "it", "possible!!" });