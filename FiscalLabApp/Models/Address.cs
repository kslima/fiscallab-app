using System.ComponentModel.DataAnnotations;

namespace FiscalLabApp.Models;

public class Address
{
    [Required(ErrorMessage = "Cidade é obrigatório.")]
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}