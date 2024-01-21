namespace FiscalLabApp.Models;

public class Plant
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public Address Address { get; set; } = null!;
}