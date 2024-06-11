using FiscalLabApp.Models;

namespace FiscalLabApp.Interfaces;

public interface IVisitContextAccessor
{
    public Visit? SelectedVisit { get; set; }
}