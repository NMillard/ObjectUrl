namespace ObjectUrl.Core.Credentials;

/// <summary>
/// 
/// </summary>
public interface IClientCredentials
{
    /// <summary>
    /// 
    /// </summary>
    public string Type { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    void AddAuthentication(HttpClient client);
}