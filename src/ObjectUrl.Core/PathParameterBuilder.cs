using System.Reflection;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public static class PathParameterBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildParameterString<T>(HttpRequest<T> httpRequest)
    {
        Dictionary<string, string?> pathParameters = httpRequest.PathParameters;
        
        var attribute = httpRequest.GetType().GetCustomAttribute<EndpointAttribute>();
        if (attribute is null) throw new InvalidOperationException(string.Format(Messages.MissingEndpoint, httpRequest.GetType().Name));

        string path = attribute.RelativePath;
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