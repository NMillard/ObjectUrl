using System.Collections;

namespace ObjectUrl.Core.Formatters;

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
        IEnumerable parameters
    );
}