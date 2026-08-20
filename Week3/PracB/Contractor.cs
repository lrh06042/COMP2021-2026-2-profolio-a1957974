public class Contractor : Employee, IReportable
{
    public decimal Rate { get; set; }

    public decimal Hours { get; set; }

    public override decimal CalculatePay()
    {
        decimal grossPay = Rate * Hours;
        decimal tax = grossPay * TaxRate;

        return grossPay - tax;
    }

    public string GenerateReport()
    {
        decimal grossPay = Rate * Hours;
        decimal tax = grossPay * TaxRate;

        return $"{Name}: Pay ${grossPay:F2}. " +
               $"Tax ${tax:F2}. Net Pay ${CalculatePay():F2}.";
    }
}