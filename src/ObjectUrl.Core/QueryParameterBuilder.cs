using System.Text;
using System.Web;

namespace ObjectUrl.Core;

/// <summary>
/// Provides extension methods to build the query parameter string from an <see cref="Input{T}"/>.
/// </summary>
public static class QueryParameterBuilder
{
    /// <summary>
    /// Build a query string from an <see cref="Input{T}"/>.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string BuildQueryString<T>(Input<T> input)
    {
        IEnumerable<(string Key, string? Value)> inputQueryParameters = input.QueryParameters.ToList();
        if (!inputQueryParameters.Any()) return "";
        
        var queryString = new StringBuilder("?");

        foreach ((string Key, string? Value) param in inputQueryParameters)
        {
            bool notFirstParameter = queryString.Length > 1;
            if (notFirstParameter) queryString.Append('&');

            queryString.Append(UrlEncode(param.Key));
            queryString.Append('=');
            queryString.Append(UrlEncode(param.Value));
        }

        return queryString.ToString();
    }

    private static string? UrlEncode(string? value)
        => HttpUtility.UrlEncode(value);
}