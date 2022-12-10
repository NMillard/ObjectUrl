namespace ObjectUrl.Core.Formatters;

/// <summary>
/// Formats the value to an ISO date format, that is full year, month, and day
/// with hyphens between the date values (YYYY-MM-DD).
/// </summary>
public class IsoDateOnlyFormatter : ValueFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public override string Format(object value)
    {
        if (value is not DateOnly date) throw new InvalidOperationException();
        return date.ToString("yyyy-MM-dd");
    }
}