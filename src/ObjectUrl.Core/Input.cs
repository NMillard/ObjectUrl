using System.Collections;
using System.Reflection;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public class QueryListAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="delimiter"></param>
    public QueryListAttribute(string delimiter = ",")
    {
        Delimiter = delimiter;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Delimiter { get; }
}

/// <summary>
/// 
/// </summary>
public abstract class Input<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public IEnumerable<(string Key, string? Value)> QueryParameters
    {
        get
        {
            var map = new List<(string key, string? value)>();
            IEnumerable<PropertyInfo> props = GetType().GetProperties()
                .Where(HasQueryAttribute())
                .Where(HasValue());
                
            foreach (PropertyInfo info in props)
            {
                QueryParameterAttribute attribute = GetQueryAttribute(info) ?? throw new InvalidOperationException();
                object? value = info.GetValue(this);
                if (value is null) continue;
                
                if (value is IEnumerable list and not string)
                {
                    var delimiter = info.GetCustomAttribute<QueryListAttribute>();
                    if (delimiter is null)
                    {
                        var parameters = from object o in list select (attribute.Name, attribute.Format(o));
                        map.AddRange(parameters);
                    }
                    else
                    {
                        IEnumerable<string> parameters = from object o in list select attribute.Format(o);
                        string combinedParameters = string.Join(delimiter.Delimiter, parameters);
                        map.Add((attribute.Name, string.Join(',', combinedParameters)));
                    }
                }
                else
                {
                    (string info, string?) parameter = (attribute.Name, attribute.Format(value!));
                    map.Add(parameter);
                }
            }

            return map;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, string?> PathParameters
    {
        get
        {
            return GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<PathParameterAttribute>() is not null)
                .Select(p =>
                {
                    var parameter = p.GetCustomAttribute<PathParameterAttribute>();
                    string pathName = parameter?.Name ?? p.Name;
                
                    return new { PathName = pathName, Value = p.GetValue(this)?.ToString() };
                })
                .ToDictionary(p => p.PathName, p => p.Value);
        }
    }
    
    private static QueryParameterAttribute? GetQueryAttribute(MemberInfo info) 
        => info.GetCustomAttribute(typeof(QueryParameterAttribute)) as QueryParameterAttribute;

    private static Func<PropertyInfo, bool> HasQueryAttribute()
        => prop => prop.GetCustomAttribute(typeof(QueryParameterAttribute), true) is {};

    private Func<PropertyInfo, bool> HasValue()
        => prop => prop.GetValue(this) != null;
}