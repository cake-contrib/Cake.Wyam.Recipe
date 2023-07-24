#load nuget:?package=Cake.Recipe&version=3.0.1

//*************************************************************************************************
// Settings
//*************************************************************************************************

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Wyam.Recipe",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.Wyam.Recipe",
    appVeyorAccountName: "cakecontrib",
    nuspecFilePath: "./Cake.Wyam.Recipe/Cake.Wyam.Recipe.nuspec",
    shouldPostToGitter: false);  // Disabled because it's currently failing

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

//*************************************************************************************************
// Extensions
//*************************************************************************************************

BuildParameters.Tasks.CleanTask
    .IsDependentOn("Generate-Version-File");

Task("Generate-Version-File")
    .Does<BuildVersion>((context, buildVersion) => {
        // Write metadata to configuration file
        System.IO.File.WriteAllText(
            "./Cake.Wyam.Recipe/cake-version.yml",
            @"TargetCakeVersion: 1.0.0
TargetFrameworks:
- net461
- netcoreapp2.0
- netcoreapp2.1
- netcoreapp3.0
- netcoreapp3.1
- net5.0"
        );

        // Write metadata to class for use when running a build
        var buildMetaDataCodeGen = TransformText(@"
        public class BuildMetaData
        {
            public static string Date { get; } = ""<%date%>"";
            public static string Version { get; } = ""<%version%>"";
            public static string CakeVersion { get; } = ""<%cakeversion%>"";
        }",
        "<%",
        "%>"
        )
   .WithToken("date", BuildMetaData.Date)
   .WithToken("version", buildVersion.SemVersion)
   .WithToken("cakeversion", BuildMetaData.CakeVersion)
   .ToString();

    System.IO.File.WriteAllText(
        "./Cake.Wyam.Recipe/Content/version.cake",
        buildMetaDataCodeGen
        );
    });

//*************************************************************************************************
// Execution
//*************************************************************************************************

Build.RunNuGet();
