namespace FiscalLabApp.Models;

public class ApiResponse<TData>
{
    public bool IsSuccess { get; set; }
    public TData? Data { get; set; }
}