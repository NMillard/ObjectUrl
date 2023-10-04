namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EndpointAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public EndpointAttribute(string path)
    {
        this.Path = path;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string Path { get; }
}