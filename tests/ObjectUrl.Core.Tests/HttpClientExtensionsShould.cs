using System.Net.Http.Headers;
using ObjectUrl.Core.Extensions;
using FluentAssertions;
using ObjectUrl.Core.Authorization;

namespace ObjectUrl.Core.Tests;

public class HttpClientExtensionsShould
{
    private static readonly HttpClient Client = new();

    [Fact]
    public void AddBasicAuthorization()
    {
        // Arrange
        var clientAuthorization = new BasicAuthorization("demo", "test");
        
        // Act
        HttpClient sut = Client.AddAuthorization(clientAuthorization);

        // Assert
        AuthenticationHeaderValue? result = sut.DefaultRequestHeaders.Authorization;
        result.Should().NotBeNull();
        result?.Scheme.Should().Be("Basic");
        result?.Parameter.Should().Be("ZGVtbzp0ZXN0");
    }
    
    [Fact]
    public void AddBearerAuthorization()
    {
        // Arrange
        var bearerAuthorization = new BearerAuthorization("mytoken");
        
        // Act
        HttpClient sut = Client.AddAuthorization(bearerAuthorization);

        // Assert
        AuthenticationHeaderValue? result = sut.DefaultRequestHeaders.Authorization;
        result?.Scheme.Should().Be("Bearer");
        result?.Parameter.Should().Be("mytoken");
    }

    [Fact]
    public void UseLatestAuthorizationAdded()
    {
        // Arrange
        HttpClient sut = Client.AddAuthorization(new BearerAuthorization("mytoken"));
        var newAuthorization = new BasicAuthorization("demo", "test");
        
        // Act
        sut.AddAuthorization(newAuthorization);
            
        // Assert
        AuthenticationHeaderValue? result = sut.DefaultRequestHeaders.Authorization;
        result.Should().NotBeNull();
        result?.Scheme.Should().Be("Basic");
        result?.Parameter.Should().Be("ZGVtbzp0ZXN0");
    }

    [Fact]
    public void ClearAuthorizationHeader()
    {
        // Arrange
        HttpClient sut = Client.AddAuthorization(new BearerAuthorization("mytoken"));
        
        // Act
        sut.ClearAuthorization();
        
        // Assert
        AuthenticationHeaderValue? result = sut.DefaultRequestHeaders.Authorization;
        result.Should().BeNull();
    }
}