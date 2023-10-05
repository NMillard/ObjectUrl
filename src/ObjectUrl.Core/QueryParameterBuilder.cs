using System.Web;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
public static class QueryParameterBuilder
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string BuildQueryString<T>(Input<T> input)
    {
        IEnumerable<string> queryString = input.QueryParameters
            .Select(prop =>
            {
                return $"{UrlEncode(prop.Key)}={UrlEncode(prop.Value)}";
            });
        string result = string.Join("&", queryString);

        return $"?{result}";
    }

    private static string? UrlEncode(string? value)
        => HttpUtility.UrlEncode(value);
}