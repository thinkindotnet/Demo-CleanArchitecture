﻿namespace MyApp.Application.Common.Models;

public class Result
{
    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }


    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public static Result Success()
    {
        return new Result(succeeded: true, Array.Empty<string>());
    }


    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(succeeded: false, errors);
    }

}
