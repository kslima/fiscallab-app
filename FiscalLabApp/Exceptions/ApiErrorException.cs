namespace FiscalLabApp.Exceptions;

public class ApiErrorException : Exception
{
    public ApiErrorException()
    {
    }
    
    public ApiErrorException(string? message) : base(message)
    {
    }

    public ApiErrorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}