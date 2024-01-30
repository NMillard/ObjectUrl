using ObjectUrl.Core.Formatters;

namespace ObjectUrl.Core;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class QueryParameterAttribute : Attribute
{
    private readonly ValueFormatter formatter = new DefaultValueFormatter();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    public QueryParameterAttribute(string name, Type? type = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        Name = name;

        if (type is null || !typeof(ValueFormatter).IsAssignableFrom(type)) return;
        
        ValueFormatter valueFormatter =  Activator.CreateInstance(type) as ValueFormatter
                                         ?? formatter;
        formatter = valueFormatter;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string? Format(object value)
        => formatter.Format(value);
}

/// <summary>
/// Type parameterized version of the base class <see cref="QueryParameterAttribute"/>.
/// The <typeparamref name="T"/> is passed as the type argument to the base constructor.
/// </summary>
/// <typeparam name="T">The formatter used to perform formatting on the value.</typeparam>
public class QueryParameterAttribute<T> : QueryParameterAttribute 
    where T : ValueFormatter
{

    /// <summary>
    /// Calls the base class constructor with <typeparamref name="T"/> as the type argument.
    /// </summary>
    /// <param name="name">The query string's key.</param>
    public QueryParameterAttribute(string name) : base(name, typeof(T)) { }
}