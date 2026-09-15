using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var numbers = new LinkedList<int>();

        // AddFirst()
        numbers.AddFirst(30);
        numbers.AddFirst(20);
        numbers.AddFirst(10);

        // AddLast()
        numbers.AddLast(40);
        numbers.AddLast(50);
        numbers.AddLast(60);
        numbers.AddLast(70);

        // Find a node so AddBefore() and AddAfter() can use its reference
        LinkedListNode<int>? node40 = numbers.Find(40);

        if (node40 != null)
        {
            // AddBefore()
            numbers.AddBefore(node40, 35);

            // AddAfter()
            numbers.AddAfter(node40, 45);
        }

        LinkedListNode<int>? node60 = numbers.Find(60);

        if (node60 != null)
        {
            numbers.AddAfter(node60, 65);
        }

        Console.WriteLine("List after inserting 10 values:");
        Console.WriteLine(string.Join(" -> ", numbers));

        // Remove a specific value
        numbers.Remove(35);

        // Remove the first node
        numbers.RemoveFirst();

        // Remove the last node
        numbers.RemoveLast();

        Console.WriteLine("\nAfter using the three removal methods:");
        Console.WriteLine(string.Join(" -> ", numbers));

        // Find the 5th element by moving through node references
        LinkedListNode<int>? currentNode = numbers.First;

        for (int position = 1;
             position < 5 && currentNode != null;
             position++)
        {
            currentNode = currentNode.Next;
        }

        if (currentNode != null)
        {
            Console.WriteLine($"\nRemoving the 5th element: {currentNode.Value}");
            numbers.Remove(currentNode);
        }

        Console.WriteLine("\nFinal LinkedList:");
        Console.WriteLine(string.Join(" -> ", numbers));
               Console.WriteLine("\n--- Task 2: Collections of Classes ---");

        Person fred = new Person("Fred", "Smith", 21);
        Person abby = new Person("Abby", "Brown", 18);
        Person bill = new Person("Bill", "Davis", 18);
        Person emma = new Person("Emma", "Wilson", 25);
        Person jack = new Person("Jack", "Taylor", 16);
        Person olivia = new Person("Olivia", "Anderson", 31);
        Person noah = new Person("Noah", "Thomas", 12);
        Person sophia = new Person("Sophia", "Moore", 45);
        Person liam = new Person("Liam", "Martin", 27);
        Person mia = new Person("Mia", "Clark", 20);

        List<Person> peopleList = new List<Person>
        {
            fred,
            abby,
            bill,
            emma,
            jack,
            olivia,
            noah,
            sophia,
            liam,
            mia
        };

               Console.WriteLine(
            $"People added to List<Person>: {peopleList.Count}");

        var peopleLinkedList = ToLinkedList(peopleList);

        Console.WriteLine(
            $"People converted to LinkedList<Person>: {peopleLinkedList.Count}");

        Console.WriteLine("\nPeople in the LinkedList:");
        Console.WriteLine(PrintPeople(peopleLinkedList));
                Console.WriteLine("\nPeople in the LinkedList:");
                        fred.Age = 22;

        Console.WriteLine("\nAfter changing Fred's age:");
        Console.WriteLine(
            $"Fred's age in List: {peopleList[0].Age}");
        Console.WriteLine(
            $"Fred's age in LinkedList: {peopleLinkedList.First?.Value.Age}");
                    
                        var sortedPeople = SortPeople(peopleLinkedList);

        Console.WriteLine("\nPeople sorted by age:");
        Console.WriteLine(PrintPeople(sortedPeople));
        Console.WriteLine(PrintPeople(peopleLinkedList));
    }

    public static LinkedList<Person> ToLinkedList(
        List<Person> people)
    {
        LinkedList<Person> linkedPeople =
            new LinkedList<Person>();

        foreach (Person person in people)
        {
            linkedPeople.AddLast(person);
        }

                return linkedPeople;
    }

    public static string PrintPeople(
        LinkedList<Person> people)
    {
        string output = string.Empty;

        foreach (Person person in people)
        {
            output +=
                $"{person.FullName}, Age: {person.Age}\n";
        }

        return output;
    }

       public static LinkedList<Person> SortPeople(
        LinkedList<Person> people)
    {
        LinkedList<Person> sortedPeople =
            new LinkedList<Person>();

        int numberOfPeople = people.Count;

        for (int i = 0; i < numberOfPeople; i++)
        {
            double youngestAge = double.MaxValue;
            Person? youngestPerson = null;

            foreach (Person person in people)
            {
                if (person.Age < youngestAge)
                {
                    youngestAge = person.Age;
                    youngestPerson = person;
                }
            }

            if (youngestPerson != null)
            {
                sortedPeople.AddLast(youngestPerson);
                people.Remove(youngestPerson);
            }
        }

        return sortedPeople;
    }
}
