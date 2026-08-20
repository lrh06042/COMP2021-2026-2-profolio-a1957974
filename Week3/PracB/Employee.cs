public abstract class Employee
{
    public string Name { get; set; } = string.Empty;

    public const decimal TaxRate = 0.2m;

    public abstract decimal CalculatePay();
}