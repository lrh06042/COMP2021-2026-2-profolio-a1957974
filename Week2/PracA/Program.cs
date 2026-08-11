const decimal INITIAL_TAX_RATE = 0.20m;

try
{
    Console.Write("Enter employee name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Hours worked: ");
    double hours = double.Parse(Console.ReadLine() ?? "");

    Console.Write("Hourly rate: ");
    decimal rate = decimal.Parse(Console.ReadLine() ?? "");

    Payroll payroll = new Payroll(hours, rate, INITIAL_TAX_RATE);

    decimal netPay = payroll.CalculateNetPay();

    Console.WriteLine(
        $"{name} earned ${netPay:F2} after 20% tax.");

    payroll.ChangeTaxRate(0.15m);

    decimal updatedNetPay = payroll.CalculateNetPay();

    Console.WriteLine(
        $"After changing the tax rate to 15%, {name} earned ${updatedNetPay:F2}.");
}
catch (FormatException)
{
    Console.WriteLine(
        "Invalid input. Please enter numbers for hours and rate.");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine(ex.Message);
}