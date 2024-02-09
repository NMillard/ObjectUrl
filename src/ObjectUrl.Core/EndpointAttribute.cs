namespace ObjectUrl.Core;

/// <summary>
/// Defines the endpoint that the request is sent to. The endpoint is specified as the relative path.<br/>
/// The HTTP client must be configured with a base url.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EndpointAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="relativePath">Path the request is made to, relative to the base url.</param>
    public EndpointAttribute(string relativePath)
    {
        RelativePath = relativePath;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string RelativePath { get; }
}