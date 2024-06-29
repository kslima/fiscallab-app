using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class ImageCard : ComponentBase
{
    [Parameter] public string ImageSrc { get; set; } = string.Empty;
    [Parameter] public string ImageName { get; set; } = string.Empty;
    [Parameter] public string Description { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> OnDelete { get; set; }

    private void OnDeleteButtonClickHandler()
    {
        OnDelete.InvokeAsync(ImageName);
    }
}