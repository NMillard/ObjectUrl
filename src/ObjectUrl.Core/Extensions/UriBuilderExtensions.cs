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
    public static UriBuilder AddQueryParameters(this UriBuilder builder, Input input)
    {
        builder.Query = ObjectParameterBuilder.BuildQueryString(input);
        return builder;
    }
}