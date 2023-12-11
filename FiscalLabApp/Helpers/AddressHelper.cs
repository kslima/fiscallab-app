namespace FiscalLabApp.Helpers;

public static class AddressHelper
{
    public static readonly IReadOnlyDictionary<string, string> StatesFromBrazil = new Dictionary<string, string>
    {
        { "AC", "Acre" },
        { "AL", "Alagoas" },
        { "AP", "Amapá" },
        { "AM", "Amazonas" },
        { "BA", "Bahia" },
        { "CE", "Ceara" },
        { "DF", "Distrito Federal" },
        { "ES", "Espirito Santo" },
        { "GO", "Goiás" },
        { "MA", "Maranhão" },
        { "MT", "Mato Grosso" },
        { "MS", "Mato Grosso do Sul" },
        { "MG", "Minas Gerais" },
        { "PA", "Para" },
        { "PB", "Paraíba" },
        { "PR", "Paraná" },
        { "PE", "Pernambuco" },
        { "PI", "Piauí" },
        { "RJ", "Rio de Janeiro" },
        { "RN", "Rio Grande do Norte" },
        { "RS", "Rio Grande do Sul" },
        { "RO", "Rondônia" },
        { "RR", "Roraima" },
        { "SC", "Santa Catarina" },
        { "SP", "São Paulo" },
        { "SE", "Sergipe" },
        { "TO", "Tocantins" },
    };
}