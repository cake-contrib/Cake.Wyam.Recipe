///////////////////////////////////////////////////////////////////////////////
// TOOLS
///////////////////////////////////////////////////////////////////////////////

private const string GitVersionTool = "#tool nuget:?package=GitVersion.CommandLine&version=3.6.2";
private const string KuduSyncTool = "#tool nuget:?package=KuduSync.NET&version=1.3.1";
private const string WyamTool = "#tool nuget:?package=Wyam&version=0.17.7";

Action<string, Action> RequireTool = (tool, action) => {
    var script = MakeAbsolute(File(string.Format("./{0}.cake", Guid.NewGuid())));
    try
    {
        System.IO.File.WriteAllText(script.FullPath, tool);
        CakeExecuteScript(script);
    }
    finally
    {
        if (FileExists(script))
        {
            DeleteFile(script);
        }
    }

    action();
};
