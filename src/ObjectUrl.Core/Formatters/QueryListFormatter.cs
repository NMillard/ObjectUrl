using System.Collections;

namespace ObjectUrl.Core.Formatters;

/// <summary>
/// 
/// </summary>
public abstract class QueryListFormatter : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public abstract IEnumerable<(string Name, string? Value)> Format(
        QueryParameterAttribute attribute,
        IEnumerable parameters
    );
}