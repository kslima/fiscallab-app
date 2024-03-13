using System.ComponentModel.DataAnnotations;

namespace FiscalLabApp.Models;

public class BasicInformationViewModel
{
    [Required(ErrorMessage = "Usina é obrigatório")]
    public Plant? Plant { get; set; }
    [Required(ErrorMessage = "Associação é obrigatório")]
    public Association? Association { get; set; }
    public string Consultant { get; set; } = string.Empty;
    public string Inspector { get; set; } = string.Empty;
    public string Leader { get; set; } = string.Empty;
    public string LaboratoryLeader { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data é obrigatório")]
    public DateOnly VisitDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required(ErrorMessage = "Hora é obrigatório")]
    public TimeOnly VisitTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
}