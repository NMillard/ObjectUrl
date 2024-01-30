namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public interface IQueryListFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public IEnumerable<(string Name, string? Value)> Format(
        QueryParameterAttribute attribute,
        IEnumerable<object> parameters
    );
}

/// <summary>
/// 
/// </summary>
public class DelimitedQueryListFormatter : Attribute, IQueryListFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="delimiter"></param>
    public DelimitedQueryListFormatter(string delimiter = ",")
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

/// <summary>
/// 
/// </summary>
public class NullListFormatter : IQueryListFormatter
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