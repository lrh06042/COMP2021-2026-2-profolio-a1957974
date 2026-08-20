FullTimeEmployee fullTimeEmployee = new FullTimeEmployee
{
    Name = "Bill",
    AnnualSalary = 60000m
};

Contractor contractor = new Contractor
{
    Name = "Fred",
    Rate = 50m,
    Hours = 100m
};

Console.WriteLine("=== Full-Time Employee ===");
Console.WriteLine(fullTimeEmployee.GenerateReport());
Console.WriteLine(
    $"CalculatePay(): ${fullTimeEmployee.CalculatePay():F2}"
);

Console.WriteLine();

Console.WriteLine("=== Contractor ===");
Console.WriteLine(contractor.GenerateReport());
Console.WriteLine(
    $"CalculatePay(): ${contractor.CalculatePay():F2}"
);
