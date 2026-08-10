using Xunit;

public class PersonTests
{
    [Fact]
    public void FullName_ReturnsExpectedFormat()
    {
        Person person = new Person("Casey", "Smith", 20);

        string result = person.FullName();

        Assert.Equal("Smith, Casey", result);
    }

    [Fact]
    public void IsAdult_ReturnsTrue_WhenAge18OrMore()
    {
        Person person = new Person("Casey", "Smith", 18);

        bool result = person.IsAdult();

        Assert.True(result);
    }
}