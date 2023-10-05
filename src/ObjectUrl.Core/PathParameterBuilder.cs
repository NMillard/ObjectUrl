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
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildParameterString<T>(Input<T> input)
    {
        Dictionary<string, string?> pathParameters = input.PathParameters;
        
        var attribute = input.GetType().GetCustomAttribute<EndpointAttribute>();
        if (attribute is null) throw new InvalidOperationException(string.Format(Messages.MissingEndpoint, input.GetType().Name));

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