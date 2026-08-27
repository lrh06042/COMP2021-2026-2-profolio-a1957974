using Xunit;

public class EmployeeTests
{
    [Fact]
    public void Employee_Name_CanBeSetAndRead()
    {
        FullTimeEmployee employee = new FullTimeEmployee
        {
            Name = "Bill"
        };

        Assert.Equal("Bill", employee.Name);
    }

    [Fact]
    public void Employee_TaxRate_IsTwentyPercent()
    {
        Assert.Equal(0.2m, Employee.TaxRate);
    }

    [Fact]
    public void FullTimeEmployee_AnnualSalary_CanBeSetAndRead()
    {
        FullTimeEmployee employee = new FullTimeEmployee
        {
            AnnualSalary = 60000m
        };

        Assert.Equal(60000m, employee.AnnualSalary);
    }

    [Fact]
    public void FullTimeEmployee_CalculatePay_ReturnsCorrectNetPay()
    {
        FullTimeEmployee employee = new FullTimeEmployee
        {
            Name = "Bill",
            AnnualSalary = 60000m
        };

        decimal result = employee.CalculatePay();

        Assert.Equal(48000m, result);
    }

    [Fact]
    public void FullTimeEmployee_GenerateReport_ReturnsCorrectReport()
    {
        FullTimeEmployee employee = new FullTimeEmployee
        {
            Name = "Bill",
            AnnualSalary = 60000m
        };

        string result = employee.GenerateReport();

        Assert.Equal(
            "Bill: Pay $60000.00. Tax $12000.00. " +
            "Net Pay $48000.00.",
            result
        );
    }

    [Fact]
    public void Contractor_RateAndHours_CanBeSetAndRead()
    {
        Contractor contractor = new Contractor
        {
            Rate = 50m,
            Hours = 100m
        };

        Assert.Equal(50m, contractor.Rate);
        Assert.Equal(100m, contractor.Hours);
    }

    [Fact]
    public void Contractor_CalculatePay_ReturnsCorrectNetPay()
    {
        Contractor contractor = new Contractor
        {
            Name = "Fred",
            Rate = 50m,
            Hours = 100m
        };

        decimal result = contractor.CalculatePay();

        Assert.Equal(4000m, result);
    }

    [Fact]
    public void Contractor_GenerateReport_ReturnsCorrectReport()
    {
        Contractor contractor = new Contractor
        {
            Name = "Fred",
            Rate = 50m,
            Hours = 100m
        };

        string result = contractor.GenerateReport();

        Assert.Equal(
            "Fred: Pay $5000.00. Tax $1000.00. " +
            "Net Pay $4000.00.",
            result
        );
    }
}