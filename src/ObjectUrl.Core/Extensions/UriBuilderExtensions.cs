namespace ObjectUrl.Core.Extensions;

/// <summary>
/// 
/// </summary>
public static class UriBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static UriBuilder AddQueryParameters<T>(this UriBuilder builder, HttpRequest<T> httpRequest)
    {
        builder.Query = QueryParameterBuilder.BuildQueryString(httpRequest);
        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static UriBuilder AddEndpointPath<T>(this UriBuilder builder, HttpRequest<T> httpRequest)
    {
        builder.Path = PathParameterBuilder.BuildParameterString(httpRequest);
        return builder;
    }
}