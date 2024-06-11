using FiscalLabApp.Models;
using Microsoft.AspNetCore.Components;

namespace FiscalLabApp.Components.Shared.VisitComponents;

public partial class BasicInformationComponent : ComponentBase
{
    [Parameter]
    public BasicInformation BasicInformation { get; set; } = null!;
    [Parameter]
    public EventCallback<BasicInformation> OnClose { get; set; }
    [Parameter]
    public EventCallback OnAddAssociationButtonClick { get; set; }
    [Parameter]
    public EventCallback OnEditAssociationButtonClick { get; set; }
    [Parameter]
    public EventCallback OnAddPlantButtonClick { get; set; }
    [Parameter]
    public EventCallback OnEditPlantButtonClick { get; set; }
    [Parameter]
    public Menu[] Menus { get; set; }= [];
    [Parameter]
    public Models.Plant[] Plants { get; set; }= [];
    [Parameter]
    public Models.Association[] Associations { get; set; }= [];

    private List<VisitSelect.SelectModel> GetAssociations()
    {
        return Associations.Select(x => new VisitSelect.SelectModel(x.Id, x.Name)).ToList();
    }
    
    private List<VisitSelect.SelectModel> GetPlants()
    {
        return Plants.Select(x => new VisitSelect.SelectModel(x.Id, x.Name)).ToList();
    }
    
    private void OnPlantChanged(Models.Plant plant)
    {
        BasicInformation.Plant = plant;
    }

    private void OnAssociationChanged(Models.Association association)
    {
        BasicInformation.Association = association;
    }
    
    private void OnAssociationChangeHandler(string associationId)
    {
        BasicInformation.Association = Associations.Single(x => x.Id.Equals(associationId));
    }
    
    private async Task OnAddAssociationHandler()
    {
        await OnAddAssociationButtonClick.InvokeAsync();
    }
    
    private async Task OnEditAssociationHandler()
    {
        await OnEditAssociationButtonClick.InvokeAsync();
    }
    
    private void OnPlantChangeHandler(string plantId)
    {
        BasicInformation.Plant = Plants.Single(x => x.Id.Equals(plantId));
    }
    
    private async Task OnAddPlantHandler()
    {
        await OnAddPlantButtonClick.InvokeAsync();
    }
    
    private async Task OnEditPlantHandler()
    {
        await OnEditPlantButtonClick.InvokeAsync();
    }
}