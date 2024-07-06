using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace FiscalLabApp.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    private AuthenticationState? _authenticationState;

    private ErrorBoundary? _errorBoundary;

    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }

    protected override async Task OnInitializedAsync()
    {
        _authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    }
    
    private void ResetError() 
    { 
        _errorBoundary?.Recover(); 
    }
}