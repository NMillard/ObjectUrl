<img src="https://github.com/NMillard/ObjectUrl/blob/main/images/icon.png" width="100" height="100" alt="icon">

# ObjectUrl: Documented API objects
![build](https://github.com/NMillard/ObjectUrl/actions/workflows/build.yml/badge.svg) 
![release](https://img.shields.io/nuget/vpre/ObjectUrl.Core)

This package came into existence after having to use countless external APIs and dealing with documenting
their query parameters, path variables, and response types.

The core concept here is to bundle the API call requirements into a neat single object, which can then be employed with
the `HttpClient`. No need to build typed http clients and inject them as constructor arguments anymore.

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
In many scenarios, you will need to convert a list property into a query string. The following are the two conventional strategies:
- Duplicate key: Each list value is represented with a repeated occurrence of the property name.
- Delimited value: The property name occurs once and pairs with a delimited list of values.

ObjectUrl defaults to using a duplicate key strategy for list types, if none is defined.

```csharp
// Duplicate key strategy
[QueryParameter("values")]
public IEnumerable<string> Values { get; set; } = ["one", "two"]

// -> ?values=one&values=two


// Delimited value
[QueryParameter("values")]
[DelimitedValueStrategy(delimiter: ",")]
public IEnumerable<string> Values { get; set; } = ["one", "two"]

// Notice the comma is url encoded
// -> ?values=one%2ctwo
```

## Authorization
Adding authorization to your requests is easy. There's an `AddAuthorization(client, authorization)` extension method on the `HttpClient` class.  
> Note that the `HttpRequest` object is decoupled from the authorization, since one request may have multiple ways to authenticate.

Right off the bat, ObjectUrl supports two authorization types: `Basic` and `Bearer`.

```csharp
// Simple Basic authorization using username and password.
// The username and password is concatenated with a colon (:), turned into ASCII bytes, and then converted to base64.
client.AddAuthorization(new BasicAuthorization("username", "password"));
// -> Authorization: Basic dXNlcm5hbWU6cGFzc3dvcmQ


// Bearer
client.AddAuthorization(new BearerAuthorization("some-bearer-token"));
// -> Authorization: Bearer some-bearer-token
```



## Generate code coverage report
Run the `generate-report.sh` script.