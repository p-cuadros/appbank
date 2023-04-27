using FluentValidation.Results;

namespace apibanca.application.Exceptions;
public class AppValidationException: Exception
{
    public List<ValidationFailure> Errors;
    public AppValidationException() : base("One or more errors occurs")
    {
        Errors = new List<ValidationFailure>();
    }
    public AppValidationException(IEnumerable<ValidationFailure> errors) : this()
    {
        Errors.AddRange(errors);
    }
    public AppValidationException(IEnumerable<string> errors) : this()
    {
        int i = 0;
        foreach (var e in errors)
            Errors.Append(new ValidationFailure((++i).ToString(), e));
    }
    public AppValidationException(string propertyName, string errorMessage) : this()
    {
        Errors.Add(new ValidationFailure(propertyName, errorMessage));
    }
}

