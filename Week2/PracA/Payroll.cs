public class Payroll
{
    public double Hours { get; private set; }
    public decimal Rate { get; private set; }
    public decimal TaxRate { get; private set; }

    public Payroll(double hours, decimal rate, decimal taxRate)
    {
        if (hours < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(hours),
                "Hours cannot be negative.");
        }

        if (rate < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rate),
                "Rate cannot be negative.");
        }

        if (taxRate < 0 || taxRate > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(taxRate),
                "Tax rate must be between 0 and 1.");
        }

        Hours = hours;
        Rate = rate;
        TaxRate = taxRate;
    }

    public decimal CalculateNetPay()
    {
        decimal grossPay = (decimal)Hours * Rate;
        decimal tax = grossPay * TaxRate;

        return grossPay - tax;
    }

    public void ChangeTaxRate(decimal newTaxRate)
    {
        if (newTaxRate < 0 || newTaxRate > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(newTaxRate),
                "Tax rate must be between 0 and 1.");
        }

        TaxRate = newTaxRate;
    }
}