using ObjectUrl.Core;
using ObjectUrl.Core.Extensions;


var input = new Api2Query
{
    Id = Guid.NewGuid().ToString(),
    Amount = 100,
    CreditDebit = "Credit",
    Values = new []{"one", "two"}
};

UriBuilder uriBuilder = new UriBuilder("http://localhost:5043")
    .AddQueryParameters(input)
    .AddEndpointPath(input);

using var client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5043")
};

string result = await client.GetStringAsync(uriBuilder.Uri);
Console.WriteLine(result);

Response? result2 = await client.SendRequestAsync(input);
Console.WriteLine(result2);


public class Response
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string CreditDebit { get; set; }
}

/// <summary>
/// 
/// </summary>
[Endpoint("api/query")]
public class ApiQuery : Input<Response>
{
    /// <summary>
    /// 
    /// </summary>
    [QueryParameter("amount")]
    public required decimal Amount { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [QueryParameter("creditDebit")]
    public required string CreditDebit { get; set; }
}


[Endpoint("api/query/{id}")]
public class Api2Query : Input<Response>
{
    [PathParameter("id")]
    public string Id { get; set; }
    
    [QueryParameter("amount")]
    public required decimal Amount { get; set; }
    
    [QueryParameter("creditDebit")]
    public required string CreditDebit { get; set; }

    [QueryParameter("values")]
    public IEnumerable<string> Values { get; set; }
}