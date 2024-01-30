using System.Net.Http.Headers;
using ObjectUrl.Core.Credentials;
using ObjectUrl.Core.Extensions;
using FluentAssertions;

namespace ObjectUrl.Core.Tests;

public class HttpClientExtensionsShould
{
    private static readonly HttpClient Client = new();

    [Fact]
    public void AddBasicAuthorization()
    {
        // Act
        HttpClient sut = Client.AddAuthorization(new BasicCredentials("demo", "test"));

        // Assert
        AuthenticationHeaderValue? result = sut.DefaultRequestHeaders.Authorization;
        result.Should().NotBeNull();
        result?.Scheme.Should().Be("Basic");
        result?.Parameter.Should().Be("ZGVtbzp0ZXN0");
    }
    
    [Fact]
    public void AddBearerAuthorization()
    {
        // Act
        HttpClient sut = Client.AddAuthorization(new BearerCredentials("mytoken"));

        // Assert
        AuthenticationHeaderValue? result = sut.DefaultRequestHeaders.Authorization;
        result?.Scheme.Should().Be("Bearer");
        result?.Parameter.Should().Be("mytoken");
    }
}