#r "System.Net.Http"
using System.Net.Http;
using System.Net.Http.Headers;

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

BuildParameters.Tasks.CleanDocumentationTask = Task("Clean-Documentation")
    .IsDependentOn("Show-Info")
    .IsDependentOn("Print-AppVeyor-Environment-Variables")
    .Does(() =>
{
    EnsureDirectoryExists(BuildParameters.WyamPublishDirectoryPath);
});

BuildParameters.Tasks.BuildDocumentationTask = Task("Build-Documentation")
    .IsDependentOn("Clean-Documentation")
    .Does(() => RequireTool(BuildParameters.IsRunningOnDotNetCore ? ToolSettings.WyamGlobalTool : ToolSettings.WyamTool, () => {
        Wyam(new WyamSettings
        {
            Recipe = BuildParameters.WyamRecipe,
            Theme = BuildParameters.WyamTheme,
            OutputPath = MakeAbsolute(BuildParameters.Paths.Directories.PublishedDocumentation),
            RootPath = BuildParameters.WyamRootDirectoryPath,
            ConfigurationFile = BuildParameters.WyamConfigurationFile,
            PreviewVirtualDirectory = BuildParameters.WebLinkRoot,
            Settings = BuildParameters.WyamSettings
        });
    }));

BuildParameters.Tasks.PublishDocumentationTask = Task("Publish-Documentation")
    .IsDependentOn("Build-Documentation")
    .WithCriteria(() => BuildParameters.ShouldPublishDocumentation)
    .WithCriteria(() => DirectoryExists(BuildParameters.WyamRootDirectoryPath))
    .Does(() => RequireTool(BuildParameters.IsRunningOnDotNetCore ? ToolSettings.KuduSyncGlobalTool : ToolSettings.KuduSyncTool, () => {
        if(BuildParameters.CanUseWyam)
        {
            var sourceCommit = BuildParameters.GitProvider.GetCommit("./");

            var publishFolder = BuildParameters.WyamPublishDirectoryPath.Combine(DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Information("Publishing Folder: {0}", publishFolder);
            Information("Getting publish branch...");
            BuildParameters.GitProvider.Clone(BuildParameters.Wyam.DeployRemote, publishFolder, BuildParameters.Wyam.DeployBranch);

            Information("Sync output files...");
            Kudu.Sync(BuildParameters.Paths.Directories.PublishedDocumentation, publishFolder, new KuduSyncSettings {
                ArgumentCustomization = args=>args.Append("--ignore").AppendQuoted(".git;CNAME")
            });

            if (BuildParameters.GitProvider.HasUncommitedChanges(publishFolder))
            {
                Information("Stage all changes...");
                GitAddAll(publishFolder);

                Information("Commit all changes...");
                BuildParameters.GitProvider.Commit(
                    publishFolder,
                    sourceCommit.Name,
                    sourceCommit.Email,
                    string.Format("AppVeyor Publish: {0}\r\n{1}", sourceCommit.Sha, sourceCommit.Message)
                );

                Information("Pushing all changes...");
                BuildParameters.GitProvider.Push(publishFolder, BuildParameters.Wyam.AccessToken, "x-oauth-basic", BuildParameters.Wyam.DeployBranch);
            }
        }
        else
        {
            Warning("Unable to publish documentation, as not all Wyam Configuration is present");
        }
    }))
.OnError(exception =>
{
    Error(exception.Message);
    Information("Publish-Documentation Task failed, but continuing with next Task...");
    publishingError = true;
});

BuildParameters.Tasks.PurgeCloudflareCacheTask = Task("Purge-Cloudflare-Cache")
    .IsDependentOn("Publish-Documentation")
    .WithCriteria(() => BuildParameters.ShouldPurgeCloudflareCache)
    .Does(() =>
{
    if(BuildParameters.CanUseCloudflare)
    {
        var settings = new HttpSettings()
            .SetRequestBody("{ \"purge_everything\": true }")
            .AppendHeader("X-Auth-Email", BuildParameters.Cloudflare.AuthEmail)
            .AppendHeader("X-Auth-Key", BuildParameters.Cloudflare.AuthKey);

        var result = HttpSend(
            string.Format("https://api.cloudflare.com/client/v4/zones/{0}/purge_cache", BuildParameters.Cloudflare.ZoneId),
            "DELETE",
            settings);

        Information(result);
    }
});

BuildParameters.Tasks.PreviewDocumentationTask = Task("Preview-Documentation")
    .WithCriteria(() => DirectoryExists(BuildParameters.WyamRootDirectoryPath))
    .Does(() => RequireTool(BuildParameters.IsRunningOnDotNetCore ? ToolSettings.WyamGlobalTool : ToolSettings.WyamTool, () => {
        Information(BuildParameters.WebLinkRoot);
        var settings = new WyamSettings
        {
            Recipe = BuildParameters.WyamRecipe,
            Theme = BuildParameters.WyamTheme,
            OutputPath = MakeAbsolute(BuildParameters.Paths.Directories.PublishedDocumentation),
            RootPath = BuildParameters.WyamRootDirectoryPath,
            Preview = true,
            Watch = true,
            ConfigurationFile = BuildParameters.WyamConfigurationFile,
            Settings = BuildParameters.WyamSettings
        };

        if(BuildParameters.WebLinkRoot != "/")
        {
            settings.PreviewVirtualDirectory = BuildParameters.WebLinkRoot;
        }

        Wyam(settings);
    })
);
