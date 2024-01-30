using System.Net.Http.Headers;
using System.Text;

namespace ObjectUrl.Core.Credentials;

/// <summary>
/// 
/// </summary>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record BasicCredentials(string Username, string Password) : IClientCredentials
{
    /// <summary>
    /// 
    /// </summary>
    public string Type => "Basic";

    /// <summary>
    /// 
    /// </summary>
    public string Value => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}"));

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