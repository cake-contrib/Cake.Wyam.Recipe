public static class BuildParameters
{
    private static readonly IList<string> wyamAssemblyFiles = new List<string>();

    public static string Target { get; private set; }
    public static string Configuration { get; private set; }
    public static bool IsLocalBuild { get; private set; }
    public static bool IsRunningOnUnix { get; private set; }
    public static bool IsRunningOnWindows { get; private set; }
    public static bool IsRunningOnAppVeyor { get; private set; }
    public static bool IsPullRequest { get; private set; }
    public static bool IsMainRepository { get; private set; }
    public static bool IsMasterBranch { get; private set; }
    public static bool IsDevelopBranch { get; private set; }
    public static bool IsReleaseBranch { get; private set; }
    public static bool IsHotFixBranch { get; private set ; }
    public static bool IsTagged { get; private set; }
    public static bool IsPublishBuild { get; private set; }
    public static bool IsReleaseBuild { get; private set; }
    public static GitHubCredentials GitHub { get; private set; }
    public static AppVeyorCredentials AppVeyor { get; private set; }
    public static WyamCredentials Wyam { get; private set; }
    public static BuildVersion Version { get; private set; }
    public static BuildPaths Paths { get; private set; }
    public static BuildTasks Tasks { get; set; }
    public static DirectoryPath RootDirectoryPath { get; private set; }
    public static string Title { get; private set; }
    public static string RepositoryOwner { get; private set; }
    public static string RepositoryName { get; private set; }
    public static string AppVeyorAccountName { get; private set; }
    public static string AppVeyorProjectSlug { get; private set; }

    public static bool ShouldPublishDocumentation { get; private set; }

    public static DirectoryPath WyamRootDirectoryPath { get; private set; }
    public static DirectoryPath WyamPublishDirectoryPath { get; private set; }
    public static FilePath WyamConfigurationFile { get; private set; }
    public static string WyamRecipe { get; private set; }
    public static string WyamTheme { get; private set; }
    public static string WyamSourceFiles { get; private set; }
    public static IList<string> WyamAssemblyFiles 
    { 
        get
        {
            return wyamAssemblyFiles;
        }
    }
    public static string WebHost { get; private set; }
    public static string WebLinkRoot { get; private set; }
    public static string WebBaseEditUrl { get; private set; }

    public static IDictionary<string, object> WyamSettings 
    { 
        get
        {
            var settings =
                new Dictionary<string, object>
                {
                    { "Host",  WebHost },
                    { "LinkRoot",  WebLinkRoot },
                    { "BaseEditUrl", WebBaseEditUrl },
                    { "SourceFiles", WyamSourceFiles },
                    { "Title", Title },
                    { "IncludeGlobalNamespace", false }
                };

            if (WyamAssemblyFiles.Any()) 
            {
                settings.Add("AssemblyFiles", WyamAssemblyFiles);
            }

            return settings;
        } 
    }

    static BuildParameters()
    {
        Tasks = new BuildTasks();
    }

    public static bool CanUseGitReleaseManager
    {
        get
        {
            return !string.IsNullOrEmpty(BuildParameters.GitHub.UserName) &&
                !string.IsNullOrEmpty(BuildParameters.GitHub.Password);
        }
    }

    public static bool CanUseWyam
    {
        get
        {
            return !string.IsNullOrEmpty(BuildParameters.Wyam.AccessToken) &&
                !string.IsNullOrEmpty(BuildParameters.Wyam.DeployRemote) &&
                !string.IsNullOrEmpty(BuildParameters.Wyam.DeployBranch);
        }
    }

    public static void SetBuildVersion(BuildVersion version)
    {
        Version  = version;
    }

    public static void SetBuildPaths(BuildPaths paths)
    {
        Paths = paths;
    }

    public static void PrintParameters(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        context.Information("Printing Build Parameters...");
        context.Information("IsLocalBuild: {0}", IsLocalBuild);
        context.Information("IsPullRequest: {0}", IsPullRequest);
        context.Information("IsMainRepository: {0}", IsMainRepository);
        context.Information("IsTagged: {0}", IsTagged);
        context.Information("IsMasterBranch: {0}", IsMasterBranch);
        context.Information("IsDevelopBranch: {0}", IsDevelopBranch);
        context.Information("IsReleaseBranch: {0}", IsReleaseBranch);
        context.Information("IsHotFixBranch: {0}", IsHotFixBranch);
        context.Information("ShouldPublishDocumentation: {0}", ShouldPublishDocumentation);
        context.Information("IsRunningOnUnix: {0}", IsRunningOnUnix);
        context.Information("IsRunningOnWindows: {0}", IsRunningOnWindows);
        context.Information("IsRunningOnAppVeyor: {0}", IsRunningOnAppVeyor);
        context.Information("RepositoryOwner: {0}", RepositoryOwner);
        context.Information("RepositoryName: {0}", RepositoryName);
        context.Information("WyamRootDirectoryPath: {0}", WyamRootDirectoryPath);
        context.Information("WyamPublishDirectoryPath: {0}", WyamPublishDirectoryPath);
        context.Information("WyamConfigurationFile: {0}", WyamConfigurationFile);
        context.Information("WyamRecipe: {0}", WyamRecipe);
        context.Information("WyamTheme: {0}", WyamTheme);
        context.Information("WyamSourceFiles: {0}", WyamSourceFiles);
        context.Information("WyamAssemblyFiles: {0}", string.Join(", ", WyamAssemblyFiles));
        context.Information("Wyam Deploy Branch: {0}", Wyam.DeployBranch);
        context.Information("Wyam Deploy Remote: {0}", Wyam.DeployRemote);
        context.Information("WebHost: {0}", WebHost);
        context.Information("WebLinkRoot: {0}", WebLinkRoot);
        context.Information("WebBaseEditUrl: {0}", WebBaseEditUrl);
    }

    public static void SetParameters(
        ICakeContext context,
        BuildSystem buildSystem,
        string title,
        DirectoryPath rootDirectoryPath = null,
        string repositoryOwner = null,
        string repositoryName = null,
        string appVeyorAccountName = null,
        string appVeyorProjectSlug = null,
        bool shouldPublishDocumentation = true,
        DirectoryPath wyamRootDirectoryPath = null,
        DirectoryPath wyamPublishDirectoryPath = null,
        FilePath wyamConfigurationFile = null,
        string wyamRecipe = null,
        string wyamTheme = null,
        string wyamSourceFiles = null,
        IEnumerable<string> wyamAssemblyFiles = null,
        string webHost = null,
        string webLinkRoot = null,
        string webBaseEditUrl = null)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        Title = title;
        RootDirectoryPath = rootDirectoryPath ?? context.MakeAbsolute(context.Environment.WorkingDirectory);
        RepositoryOwner = repositoryOwner ?? string.Empty;
        RepositoryName = repositoryName ?? Title;
        AppVeyorAccountName = appVeyorAccountName ?? RepositoryOwner.Replace("-", "").ToLower();
        AppVeyorProjectSlug = appVeyorProjectSlug ?? Title.Replace(".", "-").ToLower();

        WyamRootDirectoryPath = wyamRootDirectoryPath ?? context.MakeAbsolute(RootDirectoryPath);
        WyamPublishDirectoryPath = wyamPublishDirectoryPath ?? context.MakeAbsolute(context.Directory("BuildArtifacts/temp/_PublishedDocumentation"));
        WyamConfigurationFile = wyamConfigurationFile ?? context.MakeAbsolute((FilePath)"config.wyam");
        WyamRecipe = wyamRecipe ?? "Blog";
        WyamTheme = wyamTheme ?? "CleanBlog";
        WyamSourceFiles = wyamSourceFiles ?? "../../" + RootDirectoryPath.FullPath + "/**/{!bin,!obj,!packages,!*.Tests,}/**/*.cs";

        if (wyamAssemblyFiles != null)
        {
            foreach (var assemblyFile in wyamAssemblyFiles) 
            {
                WyamAssemblyFiles.Add(assemblyFile);
            }
        }

        WebHost = webHost ?? string.Format("{0}.github.io", repositoryOwner);
        WebLinkRoot = webLinkRoot ?? "/";
        WebBaseEditUrl = webBaseEditUrl ?? string.Format("https://github.com/{0}/{1}/tree/master/input/", repositoryOwner, title);

        Target = context.Argument("target", "Default");
        Configuration = context.Argument("configuration", "Release");
        IsLocalBuild = buildSystem.IsLocalBuild;
        IsRunningOnUnix = context.IsRunningOnUnix();
        IsRunningOnWindows = context.IsRunningOnWindows();
        IsRunningOnAppVeyor = buildSystem.AppVeyor.IsRunningOnAppVeyor;
        IsPullRequest = buildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
        IsMainRepository = StringComparer.OrdinalIgnoreCase.Equals(string.Concat(repositoryOwner, "/", repositoryName), buildSystem.AppVeyor.Environment.Repository.Name);
        IsMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master", buildSystem.AppVeyor.Environment.Repository.Branch);
        IsDevelopBranch = StringComparer.OrdinalIgnoreCase.Equals("develop", buildSystem.AppVeyor.Environment.Repository.Branch);
        IsReleaseBranch = buildSystem.AppVeyor.Environment.Repository.Branch.StartsWith("release", StringComparison.OrdinalIgnoreCase);
        IsHotFixBranch = buildSystem.AppVeyor.Environment.Repository.Branch.StartsWith("hotfix", StringComparison.OrdinalIgnoreCase);
        IsTagged = (
            buildSystem.AppVeyor.Environment.Repository.Tag.IsTag &&
            !string.IsNullOrWhiteSpace(buildSystem.AppVeyor.Environment.Repository.Tag.Name)
        );
        GitHub = GetGitHubCredentials(context);
        AppVeyor = GetAppVeyorCredentials(context);
        Wyam = GetWyamCredentials(context);
        IsPublishBuild = new [] {
            "Create-Release-Notes"
        }.Any(
            releaseTarget => StringComparer.OrdinalIgnoreCase.Equals(releaseTarget, Target)
        );
        IsReleaseBuild = new [] {
            "Publish-NuGet-Packages",
            "Publish-Chocolatey-Packages",
            "Publish-GitHub-Release"
        }.Any(
            publishTarget => StringComparer.OrdinalIgnoreCase.Equals(publishTarget, Target)
        );

        SetBuildPaths(BuildPaths.GetPaths(context));

        ShouldPublishDocumentation = (!IsLocalBuild &&
                                !IsPullRequest &&
                                IsMainRepository &&
                                (IsMasterBranch || IsDevelopBranch) &&
                                shouldPublishDocumentation);

    }
}