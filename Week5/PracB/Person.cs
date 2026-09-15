using System;

public class Person
{
    private double _age;

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public double Age
    {
        get
        {
            return _age;
        }
        set
        {
            if (value <= 0.0)
            {
                throw new ArgumentException(
                    "Age should be greater than zero");
            }

            _age = value;
        }
    }

    public string FullName => $"{FirstName}, {LastName}";

    public Person(string firstName, string lastName, double age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public bool IsAdult()
    {
        return Age >= 18;
    }
}