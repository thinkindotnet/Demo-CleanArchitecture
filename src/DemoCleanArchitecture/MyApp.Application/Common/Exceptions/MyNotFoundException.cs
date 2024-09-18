namespace MyApp.Application.Common.Exceptions;

public class MyNotFoundException
    : ApplicationException
{
    /// <summary>
    ///     Initializes a new instance of the NotFoundException class 
    ///     with the specified name of the queried object and its key.
    /// </summary>
    /// <param name="key">The value by which the object is being queried.</param>
    /// <param name="objectName">Name of the queried object</param>
    public MyNotFoundException(string key, string objectName)
        : base($"Queried object {objectName} was not found.  Key: {key}")
    {
    }


    /// <summary>
    ///     Initializes a new instance of the NotFoundException class 
    ///     with the specified name of the queried object and its key.
    /// </summary>
    /// <param name="key">The value by which the object is being queried.</param>
    /// <param name="objectName">Name of the queried object</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public MyNotFoundException(string key, string objectName, Exception innerException)
        : base($"Queried object {objectName} was not found.  Key: {key}", innerException)
    {
    }

}
