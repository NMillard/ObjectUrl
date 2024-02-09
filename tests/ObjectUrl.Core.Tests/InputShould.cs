using ObjectUrl.Core.Formatters;

namespace ObjectUrl.Core.Tests;

public class InputShould
{
    [Fact]
    public void FunctionWithNoPathParameters()
    {
        var sut = new HttpRequestNoPathParameters();

        // Act
        Dictionary<string, string?> result = sut.PathParameters;
        
        Assert.Empty(result);
    }

    [Fact]
    public void ReturnPathPlaceholderParameters()
    {
        var expectedId = Guid.NewGuid();
        var sut = new HttpRequestWithPathParameters
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
        var sut = new HttpRequestWithRenamedPathParameter
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
        var sut = new HttpRequestWithQueryParameters
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
        var sut = new HttpRequestWithListQueryParameter
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
        var sut = new HttpRequestWithCommaSeparatedListQueryParameter
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
        var sut = new HttpRequestWithPipeSeparatedListQueryParameter
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
public class HttpRequestNoPathParameters : HttpRequest<string>;


[Endpoint("my/{Id}/path")]
public class HttpRequestWithPathParameters : HttpRequest<string>
{
    [PathParameter]
    public Guid Id { get; set; }
}


[Endpoint("my/{id}/path")]
public class HttpRequestWithRenamedPathParameter : HttpRequest<string>
{
    [PathParameter("id")]
    public Guid Id { get; set; }
}

public class HttpRequestWithQueryParameters : HttpRequest<string>
{
    [QueryParameter("id")]
    public Guid Id { get; set; }
}

public class HttpRequestWithListQueryParameter : HttpRequest<string>
{
    [QueryParameter("values")]
    public IEnumerable<string> Values { get; set; } = [];
}

public class HttpRequestWithCommaSeparatedListQueryParameter : HttpRequest<string>
{
    [QueryParameter("values"), DelimitedValueStrategy]
    public IEnumerable<string> Values { get; set; } = [];
}

public class HttpRequestWithPipeSeparatedListQueryParameter : HttpRequest<string>
{
    [QueryParameter("values"), DelimitedValueStrategy(delimiter: "|")]
    public IEnumerable<int> Values { get; set; } = [];
}