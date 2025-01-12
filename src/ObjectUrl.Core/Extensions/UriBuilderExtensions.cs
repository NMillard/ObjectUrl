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
    /// <param name="getHttpRequest"></param>
    /// <returns></returns>
    public static UriBuilder AddQueryParameters<T>(this UriBuilder builder, GetHttpRequest<T> getHttpRequest)
    {
        builder.Query = QueryParameterBuilder.BuildQueryString(getHttpRequest);
        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="getHttpRequest"></param>
    /// <returns></returns>
    public static UriBuilder AddEndpointPath<T>(this UriBuilder builder, GetHttpRequest<T> getHttpRequest)
    {
        builder.Path = PathParameterBuilder.BuildParameterString(getHttpRequest);
        return builder;
    }
}