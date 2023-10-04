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
    /// <param name="input"></param>
    /// <returns></returns>
    public static UriBuilder AddQueryParameters<T>(this UriBuilder builder, Input<T> input)
    {
        builder.Query = ObjectParameterBuilder.BuildQueryString(input);
        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static UriBuilder AddEndpointPath<T>(this UriBuilder builder, Input<T> input)
    {
        builder.Path = input.EndpointPath;
        return builder;
    }
}