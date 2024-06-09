using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Pages.VisitComponents;

public partial class BasicInformationComponent : ComponentBase
{
    [Parameter]
    public BasicInformation BasicInformation { get; set; } = null!;
    
    [Parameter]
    public EventCallback<BasicInformation> OnClose { get; set; }

    [Parameter]
    public Menu[] Menus { get; set; }= [];
    
    private void OnPlantChanged(Models.Plant plant)
    {
        BasicInformation.Plant = plant;
    }

    private void OnAssociationChanged(Models.Association association)
    {
        BasicInformation.Association = association;
    }
}