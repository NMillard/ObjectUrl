using System.Net.Http.Json;

namespace ObjectUrl.Core.Extensions;

/// <summary>
/// 
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> SendRequestAsync<T>(this HttpClient client, Input<T> input)
    {
        if (client.BaseAddress is null) throw new InvalidOperationException("Missing uri. The http client must be configured with a BaseAddress.");
        return await SendRequestAsync(client, client.BaseAddress, input);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="uri"></param>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> SendRequestAsync<T>(this HttpClient client, Uri uri, Input<T> input)
    {
        UriBuilder uriBuilder = new UriBuilder(uri)
            .AddQueryParameters(input)
            .AddEndpointPath(input);

        var result = await client.GetFromJsonAsync<T?>(uriBuilder.Uri);
        return result;
    }
}