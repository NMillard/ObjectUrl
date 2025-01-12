using FluentAssertions;

namespace ObjectUrl.Core.Tests;

public class QueryParameterBuilderShould
{
    [Theory]
    [MemberData(nameof(Inputs))]
    public void BuildQueryStringFromParameters(GetHttpRequest<string> getHttpRequest, string expected)
    {
        string result = QueryParameterBuilder.BuildQueryString(getHttpRequest);

        result.Should().Be(expected);
    }

    public static IEnumerable<object[]> Inputs =>
    [
        [ new SimpleGetHttpRequest { Name = "Faxe", Age = 50 }, "?name=Faxe&age=50" ],
        [ new SimpleGetHttpRequest(), ""]
    ];
}

internal class SimpleGetHttpRequest : GetHttpRequest<string>
{
    [QueryParameter("name")] public string? Name { get; set; }

    [QueryParameter("age")] public int? Age { get; set; }
}