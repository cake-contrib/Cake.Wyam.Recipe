///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Figlet&version=0.4.0
#addin nuget:?package=Cake.Git&version=0.13.0
#addin nuget:?package=Cake.Kudu&version=0.4.0
#addin nuget:?package=Cake.Wyam&version=0.17.7
#addin nuget:?package=Cake.Http&version=0.2.7

Action<string, IDictionary<string, string>> RequireAddin = (code, envVars) => {
    var script = MakeAbsolute(File(string.Format("./{0}.cake", Guid.NewGuid())));
    try
    {
        System.IO.File.WriteAllText(script.FullPath, code);
        CakeExecuteScript(script, new CakeSettings{ EnvironmentVariables = envVars });
    }
    finally
    {
        if (FileExists(script))
        {
            DeleteFile(script);
        }
    }
};
