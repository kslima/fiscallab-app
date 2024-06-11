namespace FiscalLabApp.Models;

public class Association
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Email> Emails { get; set; } = [];
    public Address Address { get; set; } = new();
}