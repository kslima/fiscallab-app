using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class ImageCard : ComponentBase
{
    [Parameter] public string ImageSrc { get; set; } = string.Empty;
    [Parameter] public string ImageName { get; set; } = string.Empty;
    [Parameter] public string Description { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> OnDelete { get; set; }
    [Parameter] public EventCallback<Tuple<string, string>> OnDescriptionChange { get; set; }
    
    private async Task OnInputHandlerAsync(ChangeEventArgs args)
    {
        var data = args.Value == null ? string.Empty : args.Value.ToString()!;
        await OnDescriptionChange.InvokeAsync(new Tuple<string, string>(ImageName, data));
    }

    private void OnDeleteButtonClickHandler()
    {
        OnDelete.InvokeAsync(ImageName);
    }
}