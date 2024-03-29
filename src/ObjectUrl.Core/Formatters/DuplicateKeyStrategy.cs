using System.Collections;

namespace ObjectUrl.Core.Formatters;

/// <summary>
/// This approach uses the same query parameter key multiple times with different values.
/// </summary>
/// <example>
/// Having two values for the query parameter "key", you will end up with a query string like this:
/// <code>?key=first&amp;key=second</code>
/// </example>
public class DuplicateKeyStrategy : QueryListFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override IEnumerable<(string Name, string? Value)> Format(QueryParameterAttribute attribute, IEnumerable parameters)
    {
        IEnumerable<(string Name, string? Value)> p = parameters
            .Cast<object>()
            .Select(o => (attribute.Name, attribute.Format(o)));
                        
        return p;
    }
}