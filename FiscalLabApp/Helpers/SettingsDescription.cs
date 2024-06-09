namespace FiscalLabApp.Helpers;

public static class SettingsDescription
{
    public const string RestoreDescription =
        "Restaurar dados do servidor para o aplicativo. Isso inclui Usinas, Associações e Opções de Respostas cadastradas. Todas as visitas não sincronizadas serão removidas.";
    public const string SyncVisitsDescription =
        "Envie todas as visitas não sincronizadas para o servidor.";
    public const string BackupDescription =
        "Faz um backup dos dados locais em um arquivo JSON.";
}