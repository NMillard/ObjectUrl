
using ObjectUrl.Core;
using ObjectUrl.Core.Extensions;

Console.WriteLine("Hello, World!");

using var client = new HttpClient();

var input = new ApiQuery
{
    Amount = 100,
    CreditDebit = "Credit"
};

UriBuilder uriBuilder = new UriBuilder("http://localhost:5043/api/query").AddQueryParameters(input);
string result = await client.GetStringAsync(uriBuilder.Uri);

Console.WriteLine(result);


/// <summary>
/// 
/// </summary>
public class ApiQuery : Input
{
    [QueryParameter("amount")]
    public decimal Amount { get; set; }
    
    [QueryParameter("creditDebit")]
    public string CreditDebit { get; set; }
}