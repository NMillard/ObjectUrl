using System.Net.Http.Json;
using ObjectUrl.Core.Authorization;

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
    /// <param name="getHttpRequest"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> SendRequestAsync<T>(this HttpClient client, GetHttpRequest<T> getHttpRequest)
    {
        if (client.BaseAddress is null) throw new InvalidOperationException("Missing uri. The http client must be configured with a BaseAddress.");
        return await SendRequestAsync(client, client.BaseAddress, getHttpRequest).ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="uri"></param>
    /// <param name="getHttpRequest"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> SendRequestAsync<T>(this HttpClient client, Uri uri, GetHttpRequest<T> getHttpRequest)
    {
        UriBuilder uriBuilder = new UriBuilder(uri)
            .AddQueryParameters(getHttpRequest)
            .AddEndpointPath(getHttpRequest);

        T? result = await client.GetFromJsonAsync<T?>(uriBuilder.Uri).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="authorization"></param>
    /// <returns></returns>
    public static HttpClient AddAuthorization(this HttpClient client, IClientAuthorization authorization)
    {
        authorization.AddAuthentication(client);
        return client;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public static HttpClient ClearAuthorization(this HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = null;
        
        return client;
    }
}