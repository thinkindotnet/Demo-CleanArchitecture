namespace MyApp.Application.Common.Models;

public class Result
{
    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }


    internal Result(
        bool succeeded, 
        IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }


    public static Result Success()
    {
        // #IDE0301: Simplify collection initialization
        // Collection Expression can be used to simplify Collection Initialization.
        // Example:
        //      return new Result(succeeded: true, Array.Empty<string>());
        // can be simplified as:
        //      return new Result(succeeded: true, []);

        return new Result(succeeded: true, Array.Empty<string>());
    }


    public static Result Failure(
        IEnumerable<string> errors)
    {
        return new Result(succeeded: false, errors);
    }

}
