namespace ObjectUrl.Core.Authorization;

/// <summary>
/// Defines an interface for adding authorization to an HTTP request.<p/>
/// </summary>
public interface IClientAuthorization
{
    /// <summary>
    /// Add authorization to the HTTP client.
    /// </summary>
    /// <param name="client"></param>
    void AddAuthentication(HttpClient client);
}