using System.Web;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public static class ObjectParameterBuilder
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string BuildQueryString(Input input)
    {
        Dictionary<string, string?> parameters = input.QueryParameters;
        IEnumerable<string> queryString = parameters
            .Select(prop => $"{UrlEncode(prop.Key)}={UrlEncode(prop.Value)}");
        string result = string.Join("&", queryString);

        return $"?{result}";
    }

    private static string? UrlEncode(string? value)
        => HttpUtility.UrlEncode(value);
}