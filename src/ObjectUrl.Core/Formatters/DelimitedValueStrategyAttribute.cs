namespace ObjectUrl.Core.Formatters;

/// <summary>
/// This approach concatenates each value into a single query parameter value.
/// <br/>
/// The delimiter defaults to a comma.
/// </summary>
/// <example>
/// Having two values for the query parameter "key", you will end up with a query string like this:
/// <code>?key=first,second</code>
/// </example>
[AttributeUsage(AttributeTargets.Property)]
public class DelimitedValueStrategyAttribute : Attribute, IQueryListFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="delimiter"></param>
    public DelimitedValueStrategyAttribute(string delimiter = ",")
    {
        Delimiter = delimiter;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Delimiter { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<(string Name, string? Value)> Format(QueryParameterAttribute attribute, IEnumerable<object> parameters)
    {
        IEnumerable<string?> p = parameters.Select(attribute.Format);
        string combinedParameters = string.Join(Delimiter, p);

        (string Name, string? combinedParameters) valueTuple = (attribute.Name, combinedParameters);
        return new[] { valueTuple };
    }
}