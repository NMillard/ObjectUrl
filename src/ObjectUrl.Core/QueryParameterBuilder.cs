using System.Text;
using System.Web;

namespace ObjectUrl.Core;

/// <summary>
/// Provides extension methods to build the query parameter string from an <see cref="HttpRequest{T}"/>.
/// </summary>
public static class QueryParameterBuilder
{
    /// <summary>
    /// Build a query string from an <see cref="HttpRequest{T}"/>.
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static string BuildQueryString<T>(HttpRequest<T> httpRequest)
    {
        IEnumerable<(string Key, string? Value)> inputQueryParameters = httpRequest.QueryParameters.ToList();
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