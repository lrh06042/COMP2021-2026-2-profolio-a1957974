public class FullTimeEmployee : Employee, IReportable
{
    public decimal AnnualSalary { get; set; }

    public override decimal CalculatePay()
    {
        decimal tax = AnnualSalary * TaxRate;
        return AnnualSalary - tax;
    }

    public string GenerateReport()
    {
        decimal tax = AnnualSalary * TaxRate;

        return $"{Name}: Pay ${AnnualSalary:F2}. " +
               $"Tax ${tax:F2}. Net Pay ${CalculatePay():F2}.";
    }
}