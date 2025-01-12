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
    /// <param name="getHttpRequest"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildParameterString<T>(GetHttpRequest<T> getHttpRequest)
    {
        Dictionary<string, string?> pathParameters = getHttpRequest.PathParameters;
        
        var attribute = getHttpRequest.GetType().GetCustomAttribute<EndpointAttribute>();
        if (attribute is null) throw new InvalidOperationException(string.Format(Messages.MissingEndpoint, getHttpRequest.GetType().Name));

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