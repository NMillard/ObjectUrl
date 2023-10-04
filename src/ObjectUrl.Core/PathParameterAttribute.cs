namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PathParameterAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public PathParameterAttribute()
    {
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public PathParameterAttribute(string name)
    {
        this.Name = name;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; }
}