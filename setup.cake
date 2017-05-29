#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

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

Build.RunNuGet();