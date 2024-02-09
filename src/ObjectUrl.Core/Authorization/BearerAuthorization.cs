using System.Net.Http.Headers;

namespace ObjectUrl.Core.Authorization;

/// <summary>
/// 
/// </summary>
/// <param name="BearerToken"></param>
public record BearerAuthorization(string BearerToken) : IClientAuthorization
{
    /// <summary>
    /// 
    /// </summary>
    private static string Type => "Bearer";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public void AddAuthentication(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            Type,
            BearerToken
        );
    }
}