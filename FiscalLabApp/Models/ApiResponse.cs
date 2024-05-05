namespace FiscalLabApp.Models;

public class ApiResponse<TData>
{
    public bool IsSuccess { get; set; }
    public TData? Data { get; set; }
    public string Error { get; set; } = string.Empty;

    public bool IsFailure()
    {
        return !IsSuccess;
    }
}