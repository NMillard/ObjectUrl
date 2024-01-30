using System.Net.Http.Headers;

namespace ObjectUrl.Core.Credentials;

/// <summary>
/// 
/// </summary>
/// <param name="BearerToken"></param>
public record BearerCredentials(string BearerToken) : IClientCredentials
{
    /// <summary>
    /// 
    /// </summary>
    public string Type => "Bearer";

    /// <summary>
    /// 
    /// </summary>
    public string Value => BearerToken;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public void AddAuthentication(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            Type,
            Value
        );
    }
}