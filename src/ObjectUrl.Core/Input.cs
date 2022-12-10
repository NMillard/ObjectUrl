using System.Reflection;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public abstract class Input
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public Dictionary<string, string?> QueryParameters
    {
        get
        {
            Dictionary<string, string?> queryParameters = GetType().GetProperties()
                .Where(HasQueryAttribute())
                .Where(HasValue())
                .ToDictionary(
                    info => GetQueryAttribute(info)?.Name ?? throw new InvalidOperationException(),
                    info =>
                    {
                        QueryParameterAttribute? attribute = GetQueryAttribute(info);
                        object? value = info.GetValue(this);

                        // Safely force non-null since null values are filtered out.
                        return attribute?.Format(value!);
                    });

            return queryParameters;
        }
    }

    private QueryParameterAttribute? GetQueryAttribute(MemberInfo info) 
        => info.GetCustomAttribute(typeof(QueryParameterAttribute)) as QueryParameterAttribute;

    private static Func<PropertyInfo, bool> HasQueryAttribute()
        => prop => prop.GetCustomAttribute(typeof(QueryParameterAttribute), true) is {};

    private Func<PropertyInfo, bool> HasValue()
        => prop => prop.GetValue(this) != null;
}