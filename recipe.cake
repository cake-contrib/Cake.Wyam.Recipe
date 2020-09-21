#load nuget:https://pkgs.dev.azure.com/cake-contrib/Home/_packaging/addins%40Local/nuget/v3/index.json?package=Cake.Recipe&version=2.0.0-alpha0471&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.Wyam.Recipe",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Wyam.Recipe",
                            appVeyorAccountName: "cakecontrib",
                            nuspecFilePath: "./Cake.Wyam.Recipe/Cake.Wyam.Recipe.nuspec");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

BuildParameters.Tasks.CleanTask
    .IsDependentOn("Generate-Version-File");

Task("Generate-Version-File")
    .Does<BuildVersion>((context, buildVersion) => {
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

Build.RunNuGet();
