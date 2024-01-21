using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class AssociationExtensions
{
    public static AssociationViewModel AsAssociationViewModel(this Association association)
    {
        return new AssociationViewModel
        {
            Id = association.Id,
            Name = association.Name,
            City = association.Address.City,
            Emails = association.Emails.Select(e => e.Address).ToList();
            State = association.Address.State
        };
    }
    
    public static Association AsAssociation(this AssociationViewModel plantViewModel)
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