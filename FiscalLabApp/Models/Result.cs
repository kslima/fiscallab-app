using System.Text.Json.Serialization;

namespace FiscalLabApp.Models;

public class Result<TResult>
{
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        if (error != Error.None)
        {
            Error = error;
        }
    }
    
    private Result(bool isSuccess, Error error, TResult data) : this(isSuccess, error)
    {
        Data = data;
    }
    
    public bool IsSuccess { get; }
    
    public TResult? Data { get; }

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;
    
    public Error? Error { get; }

    public static Result<TResult> Success(TResult data) => new(true, Error.None, data);

    public static Result<TResult> Failure(Error error) => new(false, error);
}