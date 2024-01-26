using FiscalLabApp.Models;

namespace FiscalLabApp.Extensions;

public static class AssociationExtensions
{
    private const string SplitEmailToken = ";";
    public static AssociationViewModel AsAssociationViewModel(this Association association)
    {
        var emails = $"{string.Join($"{SplitEmailToken}{Environment.NewLine}{Environment.NewLine}", association.Emails.Select(o => o.Address))}";
        if (!emails.EndsWith(SplitEmailToken))
        {
            emails += SplitEmailToken;
        }
        return new AssociationViewModel
        {
            Id = association.Id,
            Name = association.Name,
            City = association.Address.City,
            Emails = emails,
            State = association.Address.State
        };
    }
}