///////////////////////////////////////////////////////////////////////////////
// TOOLS
///////////////////////////////////////////////////////////////////////////////

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
