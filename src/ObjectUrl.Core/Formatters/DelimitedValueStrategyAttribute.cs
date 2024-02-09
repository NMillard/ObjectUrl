using System.Collections;

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
public class DelimitedValueStrategyAttribute(string delimiter = ",") : QueryListFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<(string Name, string? Value)> Format(QueryParameterAttribute attribute, IEnumerable parameters)
    {
        IEnumerable<string?> p = parameters.Cast<object>().Select(attribute.Format);
        string combinedParameters = string.Join(delimiter, p);

        (string Name, string? combinedParameters) valueTuple = (attribute.Name, combinedParameters);
        return new[] { valueTuple };
    }
}