using ObjectUrl.Core;
using ObjectUrl.Core.Extensions;

namespace Console.Requester;

public class SimpleRequest
{
    [Fact]
    public async Task ApiRequest()
    {
        var input = new MyApiRequest
        {
            Id = Guid.Parse("d65ce0f6-2cec-4a94-8a84-bee4dca75a01"),
            Amount = 100,
            CreditDebit = "Credit",
            Values = new []{"one", "two"}
        };
        
        // The "input" should generate the following path that is appended to http://localhost:5043
        // > api/query/d65ce0f6-2cec-4a94-8a84-bee4dca75a01?amount=100&creditdebit=credit&values=one&values=two

        using var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5043")
        };
        
        MyApiResponse? result = await client.SendRequestAsync(input);
    }
}



public class MyApiResponse
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string CreditDebit { get; set; }
}


[Endpoint("api/query/{id}")] // The API endpoint
public class MyApiRequest : Input<MyApiResponse> // MyApiResponse is the expected JSON format received
{
    [PathParameter("id")] // Property value replaces the path variable {id} 
    public Guid Id { get; set; }
    
    [QueryParameter("amount")]
    public required decimal Amount { get; set; }
    
    [QueryParameter("creditDebit")]
    public required string CreditDebit { get; set; }

    [QueryParameter("values")]
    public IEnumerable<string> Values { get; set; }
}