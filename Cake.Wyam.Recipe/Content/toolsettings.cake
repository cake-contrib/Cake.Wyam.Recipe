public static class ToolSettings
{
    static ToolSettings()
    {
        SetToolPreprocessorDirectives();
    }

    public static string WyamGlobalTool { get; private set; }
    public static string KuduSyncGlobalTool { get; private set; }

    public static void SetToolPreprocessorDirectives(
        string wyamGlobalTool = "#tool dotnet:?package=Wyam.Tool&version=2.2.9",
        // This is using an unofficial build of kudusync so that we can have a .Net Global tool version.  This was generated from this PR: https://github.com/projectkudu/KuduSync.NET/pull/27
        string kuduSyncGlobalTool = "#tool dotnet:https://pkgs.dev.azure.com/cake-contrib/Home/_packaging/addins/nuget/v3/index.json?package=KuduSync.Tool&version=1.5.4-g13cb5857b6"
    )
    {
        WyamGlobalTool = wyamGlobalTool;
        KuduSyncGlobalTool = kuduSyncGlobalTool;
    }
}