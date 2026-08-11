using Xunit;

public class PayrollTests
{
    [Fact]
    public void Constructor_ValidValues_SetsProperties()
    {
        Payroll payroll = new Payroll(40, 25m, 0.20m);

        Assert.Equal(40, payroll.Hours);
        Assert.Equal(25m, payroll.Rate);
        Assert.Equal(0.20m, payroll.TaxRate);
    }

    [Fact]
    public void Constructor_NegativeHours_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Payroll(-1, 25m, 0.20m));
    }

    [Fact]
    public void Constructor_NegativeRate_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Payroll(40, -1m, 0.20m));
    }

    [Fact]
    public void Constructor_NegativeTaxRate_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Payroll(40, 25m, -0.01m));
    }

    [Fact]
    public void Constructor_TaxRateAboveOne_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Payroll(40, 25m, 1.01m));
    }

    [Fact]
    public void CalculateNetPay_ReturnsCorrectAmount()
    {
        Payroll payroll = new Payroll(40, 25m, 0.20m);

        decimal result = payroll.CalculateNetPay();

        Assert.Equal(800m, result);
    }

    [Fact]
    public void ChangeTaxRate_ValidRate_UpdatesTaxRateAndNetPay()
    {
        Payroll payroll = new Payroll(40, 25m, 0.20m);

        payroll.ChangeTaxRate(0.15m);

        Assert.Equal(0.15m, payroll.TaxRate);
        Assert.Equal(850m, payroll.CalculateNetPay());
    }

    [Fact]
    public void ChangeTaxRate_NegativeRate_ThrowsException()
    {
        Payroll payroll = new Payroll(40, 25m, 0.20m);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => payroll.ChangeTaxRate(-0.01m));
    }

    [Fact]
    public void ChangeTaxRate_RateAboveOne_ThrowsException()
    {
        Payroll payroll = new Payroll(40, 25m, 0.20m);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => payroll.ChangeTaxRate(1.01m));
    }
}