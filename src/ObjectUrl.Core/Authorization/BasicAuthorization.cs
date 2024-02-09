using System.Net.Http.Headers;
using System.Text;

namespace ObjectUrl.Core.Authorization;

/// <summary>
/// The basic authorization class adds the authorization header "<c>Basic base64(ascii(username:password))</c>" to
/// the request.
/// </summary>
public record BasicAuthorization(string Username, string Password) : IClientAuthorization
{
    /// <summary>
    /// 
    /// </summary>
    private static string Type => "Basic";

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