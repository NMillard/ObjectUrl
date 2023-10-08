<img src="images/icon.png" width="100" height="100" alt="icon">

# ObjectUrl: Documented API objects
![build](https://github.com/NMillard/ObjectUrl/actions/workflows/build.yml/badge.svg) 
![release](https://img.shields.io/nuget/vpre/ObjectUrl.Core)

This package came into existence after having to use countless external APIs and dealing with documenting
their query parameters, path variables, and response types.

Encapsulate API call requirements within a single object that can be used with the `HttpClient`
without relying on building typed http clients that you inject as constructor arguments.

The idea is simple: create a class that models a request and send the request. That's it.

```csharp
[Endpoint("api/query/{id}")] // The API endpoint
public class MyApiRequest : Input<MyApiResponse> // MyApiResponse is the expected JSON format
{
    [PathParameter("id")] // Property value replaces the path variable {id} 
    public Guid Id { get; set; }
    
    [QueryParameter("amount")]
    public required decimal Amount { get; set; }
    
    [QueryParameter("credit-debit")]
    public required string CreditDebit { get; set; }
}
```

No need to register anything with the dependency injection container. Just use it with the native `HttpClient` you already
know and love.

```csharp
var input = new MyApiRequest
{
    Id = Guid.Parse("d65ce0f6-2cec-4a94-8a84-bee4dca75a01"),
    Amount = 100,
    CreditDebit = "Credit"
};

using var client = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5043")
};

MyApiResponse? result = await client.SendRequestAsync(input);
// -> http://localhost:5043/api/query/d65ce0f6-2cec-4a94-8a84-bee4dca75a01?amount=100&credit-debit=Credit
```

## Query strings that are list values
You can easily create a multi-value query string, or define your own delimiter for list values.

```csharp
// Multi-value query string
[QueryParameter("values")]
public IEnumerable<string> Values { get; set; } = new []{"one", "two"}

// -> ?values=one&values=two


// Custom value delimiter
[QueryParameter("values")]
[QueryList(delimiter: ",")]
public IEnumerable<string> Values { get; set; } = new []{"one", "two"}

// Notice the comma is url encoded
// -> ?values=one%2ctwo
```


## Generate code coverage report
Run the `generate-report.sh` script.