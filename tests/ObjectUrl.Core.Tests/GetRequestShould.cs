using System.ComponentModel.DataAnnotations;
using ObjectUrl.Core.Formatters;

namespace ObjectUrl.Core.Tests;

public class GetRequestShould
{
    [Fact]
    public void FunctionWithNoPathParameters()
    {
        var sut = new GetHttpRequestNoPathParameters();

        // Act
        Dictionary<string, string?> result = sut.PathParameters;
        
        Assert.Empty(result);
    }

    [Fact]
    public void ReturnPathPlaceholderParameters()
    {
        var expectedId = Guid.NewGuid();
        var sut = new GetHttpRequestWithPathParameters
        {
            Id = expectedId
        };

        // Act
        Dictionary<string, string?> result = sut.PathParameters;

        Assert.Contains("Id", result.Keys);
        Assert.Contains(expectedId.ToString(), result.Values);
    }
    
    [Fact]
    public void ReturnPathPlaceholderParametersWhenRenamed()
    {
        var expectedId = Guid.NewGuid();
        var sut = new GetHttpRequestWithRenamedPathParameter
        {
            Id = expectedId
        };

        // Act
        Dictionary<string, string?> result = sut.PathParameters;

        Assert.Contains("id", result.Keys);
        Assert.Contains(expectedId.ToString(), result.Values);
    }

    [Fact]
    public void ReturnSimpleQueryParameter()
    {
        var expectedId = Guid.NewGuid();
        var sut = new GetHttpRequestWithQueryParameters
        {
            Id = expectedId
        };

        // Act
        IEnumerable<(string Key, string? Value)> result = sut.QueryParameters;
        
        Assert.Collection(result, tuple =>
        {
            Assert.Equal("id", tuple.Key);
            Assert.Equal(expectedId.ToString(), tuple.Value);
        });
    }
    
    [Fact]
    public void ReturnMultiNameQueryParameter()
    {
        var expectedValues = new[] {"one", "two"};
        var sut = new GetHttpRequestWithListQueryParameter
        {
            Values = expectedValues
        };

        // Act
        IEnumerable<(string Key, string? Value)> result = sut.QueryParameters.ToList();

        Assert.Contains(result, tuple => tuple.Key == "values");
        Assert.Contains(result, tuple => tuple.Value == expectedValues[0]);
        Assert.Contains(result, tuple => tuple.Value == expectedValues[1]);
    }

    [Fact]
    public void ReturnQueryParameterListAsSingleCommaSeparatedString()
    {
        var expectedValues = new[] {"one", "two"};
        var sut = new GetHttpRequestWithCommaSeparatedListQueryParameter
        {
            Values = expectedValues
        };

        // Act
        IEnumerable<(string Key, string? Value)> result = sut.QueryParameters.ToList();

        Assert.Contains(result, tuple => tuple.Key == "values");
        Assert.Contains(result, tuple => tuple.Value == "one,two");
    }

    [Fact]
    public void ReturnsQueryParametersListAsPipeDelimitedString()
    {
        var sut = new GetHttpRequestWithPipeSeparatedListQueryParameter
        {
            Values = [1, 2]
        };
        
        // Act
        List<(string Key, string? Value)> result = sut.QueryParameters.ToList();
        
        Assert.Contains(result, tuple => tuple.Key == "values");
        Assert.Contains(result, tuple => tuple.Value == "1|2");
    }
}

[Endpoint("my/path")]
public class GetHttpRequestNoPathParameters : GetHttpRequest<string>;


[Endpoint("my/{Id}/path")]
public class GetHttpRequestWithPathParameters : GetHttpRequest<string>
{
    [PathParameter]
    public Guid Id { get; set; }
}


[Endpoint("my/{id}/path")]
public class GetHttpRequestWithRenamedPathParameter : GetHttpRequest<string>
{
    [Required]
    [PathParameter("id")]
    public Guid Id { get; set; }
}

public class GetHttpRequestWithQueryParameters : GetHttpRequest<string>
{
    [QueryParameter("id")]
    public Guid Id { get; set; }
}

public class GetHttpRequestWithListQueryParameter : GetHttpRequest<string>
{
    [QueryParameter("values")]
    public IEnumerable<string> Values { get; set; } = [];
}

public class GetHttpRequestWithCommaSeparatedListQueryParameter : GetHttpRequest<string>
{
    [QueryParameter("values"), DelimitedValueStrategy]
    public IEnumerable<string> Values { get; set; } = [];
}

public class GetHttpRequestWithPipeSeparatedListQueryParameter : GetHttpRequest<string>
{
    [QueryParameter("values"), DelimitedValueStrategy(delimiter: "|")]
    public IEnumerable<int> Values { get; set; } = [];
}