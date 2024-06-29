using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared;

public partial class ModalDialog : ComponentBase
{
    [Parameter] public string Title { get; set; } = string.Empty;

    [Parameter] public RenderFragment ChildContent { get; set; } = null!;

    private string _modalDisplay = "none;";
    private string _modalClass = string.Empty;
    private bool _showBackdrop;
    private bool _isLoading = false;

    public void Open()
    {
        _modalDisplay = "block";
        _modalClass = "show";
        _showBackdrop = true;
    }
    
    public async Task Open(Func<Task> loadingCallback)
    {
        _isLoading = true;
        _modalDisplay = "block";
        _modalClass = "show";
        _showBackdrop = true;
        
        await loadingCallback.Invoke();
        
        _isLoading = false;
        StateHasChanged();
    }

    public void Close()
    {
        _modalDisplay = "none";
        _modalClass = string.Empty;
        _showBackdrop = false;
    }
}