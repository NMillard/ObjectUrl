using FluentAssertions;

namespace ObjectUrl.Core.Tests;

public class QueryParameterBuilderShould
{
    [Theory]
    [MemberData(nameof(Inputs))]
    public void BuildQueryStringFromParameters(Input<string> input, string expected)
    {
        string result = QueryParameterBuilder.BuildQueryString(input);

        result.Should().Be(expected);
    }

    public static IEnumerable<object[]> Inputs =>
    [
        [ new SimpleInput { Name = "Faxe", Age = 50 }, "?name=Faxe&age=50" ],
        [ new SimpleInput(), ""]
    ];
}

internal class SimpleInput : Input<string>
{
    [QueryParameter("name")] public string? Name { get; set; }

    [QueryParameter("age")] public int? Age { get; set; }
}