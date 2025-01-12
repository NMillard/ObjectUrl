using System.ComponentModel.DataAnnotations;

namespace ObjectUrl.Core.Tests;

public class Experiments
{
    [Fact]
    public void Demo()
    {
        var r = new MyRequest();
        var validationContext = new ValidationContext(r);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(r, validationContext, validationResults);
    }
}

public class MyRequest : GetHttpRequest<string>
{
    [Required]
    public string Hello { get; set; }
}


public interface IInputValidator<T> where T : GetHttpRequest
{
    bool Validate(T request);
}

public class ValidateMyInput : IInputValidator<MyRequest>
{
    public bool Validate(MyRequest request)
    {
        string e = request.Hello;
        return true;
    }
}