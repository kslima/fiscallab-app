namespace FiscalLabApp.Models;

public class Image
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte[] Data { get; set; } = [];
}