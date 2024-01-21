using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class PlantExtensions
{
    public static PlantViewModel AsPlantViewModel(this Plant plant)
    {
        return new PlantViewModel
        {
            Id = plant.Id,
            Name = plant.Name,
            City = plant.Address.City,
            State = plant.Address.State
        };
    }
    
    public static Plant AsPlant(this PlantViewModel plantViewModel)
    {
        return new Plant
        {
            Id = plantViewModel.Id,
            Name = plantViewModel.Name,
            Address = new Address
            {
                City = plantViewModel.City,
                State = plantViewModel.State
            }
        };
    }
}