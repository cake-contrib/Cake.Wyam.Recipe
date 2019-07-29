///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Figlet&version=1.2.0
#addin nuget:?package=Cake.Git&version=0.19.0
#addin nuget:?package=Cake.Kudu&version=0.8.0
#addin nuget:?package=Cake.Wyam&version=2.2.5
#addin nuget:?package=Cake.Http&version=0.6.1

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
