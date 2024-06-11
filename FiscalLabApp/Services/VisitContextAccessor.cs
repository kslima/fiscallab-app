using FiscalLabApp.Interfaces;
using FiscalLabApp.Models;

namespace FiscalLabApp.Services;

public class VisitContextAccessor : IVisitContextAccessor
{
    public Visit? SelectedVisit { get; set; }
}