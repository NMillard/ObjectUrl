using FluentAssertions;

namespace ObjectUrl.Core.Tests;

public class QueryParameterBuilderShould
{
    [Theory]
    [MemberData(nameof(Inputs))]
    public void BuildQueryStringFromParameters(HttpRequest<string> httpRequest, string expected)
    {
        string result = QueryParameterBuilder.BuildQueryString(httpRequest);

        result.Should().Be(expected);
    }

    public static IEnumerable<object[]> Inputs =>
    [
        [ new SimpleHttpRequest { Name = "Faxe", Age = 50 }, "?name=Faxe&age=50" ],
        [ new SimpleHttpRequest(), ""]
    ];
}

internal class SimpleHttpRequest : HttpRequest<string>
{
    [QueryParameter("name")] public string? Name { get; set; }

    [QueryParameter("age")] public int? Age { get; set; }
}