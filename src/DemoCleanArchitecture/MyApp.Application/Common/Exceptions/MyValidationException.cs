using FluentValidation.Results;

namespace MyApp.Application.Common.Exceptions;


/// <summary>
///     Represents the validation error on the model.
/// </summary>
public class MyValidationException
    : ApplicationException
{

    public IDictionary<string, string[]> Errors { get; }


    public MyValidationException()
        : base("One or more validation errors occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }


    public MyValidationException(List<ValidationFailure> failures)
        : this()
    {
        Errors = failures
                 .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                 .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

}
