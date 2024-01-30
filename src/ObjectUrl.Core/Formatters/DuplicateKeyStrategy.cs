namespace ObjectUrl.Core.Formatters;

/// <summary>
/// This approach uses the same query parameter key multiple times with different values.
/// </summary>
/// <example>
/// Having two values for the query parameter "key", you will end up with a query string like this:
/// <code>?key=first&amp;key=second</code>
/// </example>
public class DuplicateKeyStrategy : IQueryListFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public IEnumerable<(string Name, string? Value)> Format(QueryParameterAttribute attribute, IEnumerable<object> parameters)
    {
        IEnumerable<(string Name, string? Value)> p = parameters
            .Select(o => (attribute.Name, attribute.Format(o)));
                        
        return p;
    }
}