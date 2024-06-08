using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class ListVisits : ComponentBase
{
    [Parameter]
    public List<VisitViewModel> Visits { get; set; } = [];
    
    [Parameter] public EventCallback<string> OnDeleteButtonClicked { get; set; }
    [Parameter] public EventCallback<string> OnEditButtonClicked { get; set; }
    [Parameter] public EventCallback<string> OnPdfButtonClicked { get; set; }
    [Parameter] public EventCallback<string> OnSendToEmailButtonClicked { get; set; }

    public void DeleteVisitCallback(string visitId)
    {
        OnDeleteButtonClicked.InvokeAsync(visitId);
    }
    
    public void EditVisitCallback(string visitId)
    {
        OnEditButtonClicked.InvokeAsync(visitId);
    }
    
    public void PdfVisitCallback(string visitId)
    {
        OnPdfButtonClicked.InvokeAsync(visitId);
    }
    
    private async Task SendToEmailCallback(string visitId)
    {
        await OnSendToEmailButtonClicked.InvokeAsync(visitId);
    }
}