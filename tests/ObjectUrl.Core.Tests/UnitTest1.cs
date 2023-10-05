using ObjectUrl.Core.Extensions;
using ObjectUrl.Core.Formatters;

namespace ObjectUrl.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test2()
    {
        var input = new SomeRequest
        {
            Date = new DateOnly(2022, 12, 30),
            Status = "Active",
            Number = 100
        };

        string query = QueryParameterBuilder.BuildQueryString(input);
        
        var requestUri = new UriBuilder
        {
            Query = query,
            Scheme = "https",
            Host = "localhost",
            Port = 8080
        };

        Uri res = requestUri.Uri;
    }

    [Fact]
    public void demo()
    {
        var item = new SomeRequest()
        {
            Status = "min status",
            Values = new List<string> {"one", "two"}
        };

        var p = item.QueryParameters;
        string result = QueryParameterBuilder.BuildQueryString(item);
    }

    [Fact]
    public async Task demo3()
    {
        var item = new SomeRequest
        {
            Status = "min status",
            Values = new List<string> {"one", "two"}
        };

        var client = new HttpClient()
        {
            BaseAddress = new Uri("localhost:8080"),
        };

        await client.SendRequestAsync(item);
    }
}

[Endpoint("my/{id}/query")]
public class SomeRequest : Input<string>
{
    [PathParameter("id")]
    public Guid Id { get; set; }
    
    [QueryParameter<IsoDateOnlyFormatter>("date")]
    public DateOnly Date { get; init; }

    [QueryParameter("status")]
    public required string Status { get; init; }

    [QueryParameter("number")]
    public decimal? Number { get; set; }

    [QueryParameter("values")]
    public IEnumerable<string> Values { get; set; }
}