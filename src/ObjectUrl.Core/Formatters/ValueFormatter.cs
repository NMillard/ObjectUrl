namespace ObjectUrl.Core.Formatters;

/// <summary>
/// 
/// </summary>
public abstract class ValueFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string? Format(object value);
}