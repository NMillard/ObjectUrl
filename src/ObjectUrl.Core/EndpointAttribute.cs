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
    /// <param name="relativePath"></param>
    public EndpointAttribute(string relativePath)
    {
        RelativePath = relativePath;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string RelativePath { get; }
}