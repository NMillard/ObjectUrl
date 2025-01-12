using System.Collections;
using System.Reflection;
using ObjectUrl.Core.Formatters;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public class GetHttpRequest
{
    
}


/// <summary>
/// Models an HTTP request that is sent to an API.
/// </summary>
/// <remarks>The implementing class must be annotated with the attribute <c>[Endpoint(path)]</c>.</remarks>
/// <example>
/// <code>
/// [Endpoint("api/auth-demo/{id}")]
/// public class MyRequest : HttpRequest&lt;SomeResponseModel&gt;
/// {
///    [PathParameter("id")]
///    public Guid SomeId { get; set; }
/// }
/// </code>
/// </example>
/// <typeparam name="T">The request's expected return type.</typeparam>
public abstract class GetHttpRequest<T> : GetHttpRequest
{
    private readonly QueryListFormatter defaultListFormatter = new DuplicateKeyStrategy();
    
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public IEnumerable<(string Key, string? Value)> QueryParameters
    {
        get
        {
            var map = new List<(string key, string? value)>();
            IEnumerable<PropertyInfo> props = GetType()
                .GetProperties()
                .Where(HasQueryAttribute())
                .Where(HasValue());

            foreach (PropertyInfo info in props)
            {
                QueryParameterAttribute attribute = GetQueryAttribute(info) ?? throw new InvalidOperationException();
                object? value = info.GetValue(this);
                if (value is null) continue;

                bool notListValue = value is string or not IEnumerable;
                if (notListValue)
                {
                    (string info, string?) parameter = (attribute.Name, attribute.Format(value));
                    map.Add(parameter);
                    continue;
                }

                IEnumerable list = value as IEnumerable ?? new List<object>();
                QueryListFormatter listFormatter = info.GetCustomAttribute<QueryListFormatter>()
                                                    ?? defaultListFormatter;

                IEnumerable<(string Name, string? Value)> result = listFormatter.Format(attribute, list);
                map.AddRange(result);
            }

            return map;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, string?> PathParameters => GetType().GetProperties()
        .Where(p => p.GetCustomAttribute<PathParameterAttribute>() is not null)
        .Select(p =>
        {
            var parameter = p.GetCustomAttribute<PathParameterAttribute>();
            string pathName = parameter?.Name ?? p.Name;

            return new { PathName = pathName, Value = p.GetValue(this)?.ToString() };
        })
        .ToDictionary(p => p.PathName, p => p.Value);

    private static QueryParameterAttribute? GetQueryAttribute(MemberInfo info) 
        => info.GetCustomAttribute(typeof(QueryParameterAttribute)) as QueryParameterAttribute;

    private static Func<PropertyInfo, bool> HasQueryAttribute()
        => prop => prop.GetCustomAttribute(typeof(QueryParameterAttribute), true) is not null;

    private Func<PropertyInfo, bool> HasValue()
        => prop => prop.GetValue(this) is not null;
}