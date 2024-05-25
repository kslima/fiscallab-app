using FiscalLabApp.Enums;

namespace FiscalLabApp.Models;

public class VisitParameters : AbstractQueryParameters
{
    public VisitStatus? Status { get; set; }
}