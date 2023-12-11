using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Extensions;

public static class NavigationManagerExtensions
{
    public static string GetCurrentVisitPage(this NavigationManager navigationManager)
    {
        var routeParameters = navigationManager.Uri
            .Split("/");
        return routeParameters[^1];
    }
    
    public static string GetCurrentVisitUrl(this NavigationManager navigationManager)
    {
        var routeParameters = navigationManager.Uri
            .Split("/visits/");
        
        return $"/visits/{routeParameters[^1]}";
    }
    
    public static void ReloadPage(this NavigationManager manager)
    {
        manager.NavigateTo(manager.Uri, true);
    }
}