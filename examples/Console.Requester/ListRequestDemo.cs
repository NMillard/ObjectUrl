using ObjectUrl.Core;
using ObjectUrl.Core.Extensions;
using ObjectUrl.Core.Formatters;

namespace Console.Requester;

public class ListRequestDemo
{
    [Fact]
    public async Task RequestUsingDelimitedListValues()
    {
        var input = new ListValuesRequest
        {
            Id = Guid.NewGuid(),
            Values = ["one", "two"]
        };

        // The "input" should generate the following path that is appended to http://localhost:5043
        // > api/query/d65ce0f6-2cec-4a94-8a84-bee4dca75a01?values=one,two
        
        using var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5043")
        };

        await client.SendRequestAsync(input);
    }
}

[Endpoint("api/query/{id}")] // The API endpoint
public class ListValuesRequest : HttpRequest<MyApiResponse>
{
    [PathParameter("id")]
    public Guid Id { get; set; }
    
    [DelimitedValueStrategy(delimiter: ",")]
    [QueryParameter("values")]
    public IEnumerable<string>? Values { get; set; }
}