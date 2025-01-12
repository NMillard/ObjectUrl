using ObjectUrl.Core;
using ObjectUrl.Core.Authorization;
using ObjectUrl.Core.Extensions;

namespace Console.Requester;

public class AuthorizationRequestDemo
{
    [Fact]
    public async Task BasicAuthorization()
    {
        using var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5043")
        };
        client.AddAuthorization(new BasicAuthorization("username", "password"));
        

        string? sendRequestAsync = await client.SendRequestAsync(new MyAuthorizationRequest
        {
            SomeId = Guid.Parse("960f9b36-6b8c-46a3-b33d-86c0ccc681d3")
        });
    }

    [Fact]
    public async Task BearerAuthorization()
    {
        using var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5043")
        };
        client.AddAuthorization(new BearerAuthorization("some-bearer-token"));
        
        string? sendRequestAsync = await client.SendRequestAsync(new MyAuthorizationRequest
        {
            SomeId = Guid.Parse("960f9b36-6b8c-46a3-b33d-86c0ccc681d3")
        });
    }
}

[Endpoint("api/auth-demo/{id}")]
public class MyAuthorizationRequest : GetHttpRequest<string>
{
    [PathParameter("id")]
    public Guid SomeId { get; set; }
}