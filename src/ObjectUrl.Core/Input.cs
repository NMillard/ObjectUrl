using System.Reflection;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public abstract class Input<T>
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

    /// <summary>
    /// 
    /// </summary>
    public string EndpointPath
    {
        get
        {
            var attribute = GetType().GetCustomAttribute<EndpointAttribute>();
            if (attribute is null) throw new InvalidOperationException(string.Format(Messages.MissingEndpoint, GetType().Name));

            Dictionary<string, string?> pathParameters = GetPathParameters();
            
            string path = attribute.Path;
            foreach (KeyValuePair<string, string?> parameter in pathParameters)
            {
                var placeholder = $"{{{parameter.Key}}}";
                if (path.Contains(placeholder))
                {
                    path = path.Replace(placeholder, parameter.Value);
                }
            }

            return path;
        }
    }

    private Dictionary<string, string?> GetPathParameters()
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

    private static QueryParameterAttribute? GetQueryAttribute(MemberInfo info) 
        => info.GetCustomAttribute(typeof(QueryParameterAttribute)) as QueryParameterAttribute;

    private static Func<PropertyInfo, bool> HasQueryAttribute()
        => prop => prop.GetCustomAttribute(typeof(QueryParameterAttribute), true) is {};

    private Func<PropertyInfo, bool> HasValue()
        => prop => prop.GetValue(this) != null;
}