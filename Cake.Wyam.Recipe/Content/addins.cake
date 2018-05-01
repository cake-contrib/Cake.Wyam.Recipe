///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Figlet&version=1.1.0
#addin nuget:?package=Cake.Git&version=0.17.0
#addin nuget:?package=Cake.Kudu&version=0.7.0
#addin nuget:?package=Cake.Wyam&version=1.4.1
#addin nuget:?package=Cake.Http&version=0.5.0

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
