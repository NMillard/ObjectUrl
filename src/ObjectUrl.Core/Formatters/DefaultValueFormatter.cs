namespace ObjectUrl.Core.Formatters;

/// <summary>
/// 
/// </summary>
public class DefaultValueFormatter : ValueFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override string? Format(object value)
        => value.ToString();
}