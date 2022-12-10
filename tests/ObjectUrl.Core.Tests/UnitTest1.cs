using ObjectUrl.Core.Formatters;

namespace ObjectUrl.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Dictionary<string, string?> expected = new()
        {
            {"date", "2022-12-30"},
            {"status", "Active"}
        };

        var input = new SomeRequest
        {
            Date = new DateOnly(2022, 12, 30),
            Status = "Active",
        };



        // Act
        Dictionary<string, string?> parameters = input.QueryParameters;

        Assert.Equal(expected, parameters);
    }

    [Fact]
    public void Test2()
    {
        var input = new SomeRequest
        {
            Date = new DateOnly(2022, 12, 30),
            Status = "Active",
            Number = 100
        };

        string query = ObjectParameterBuilder.BuildQueryString(input);
        
        var requestUri = new UriBuilder
        {
            Query = query,
            Scheme = "https",
            Host = "localhost",
            Port = 8080
        };

        Uri res = requestUri.Uri;
    }
}

public class SomeRequest : Input
{
    [QueryParameter<IsoDateOnlyFormatter>("date")]
    public DateOnly Date { get; init; }

    [QueryParameter("status")]
    public required string Status { get; init; }

    [QueryParameter("number")]
    public decimal? Number { get; set; }
}