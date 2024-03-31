using System.ComponentModel.DataAnnotations;

namespace FiscalLabApp.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email náo é valido.")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Senha é obrigatório")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Senha precisa ter entre 6 e 50 caracteres.")]
    public string Password { get; set; } = string.Empty;
}