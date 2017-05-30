public static class Environment
{
    public static string GithubUserNameVariable { get; private set; }
    public static string GithubPasswordVariable { get; private set; }
    public static string AppVeyorApiTokenVariable { get; private set; }
    public static string WyamAccessTokenVariable { get; private set; }
    public static string WyamDeployRemoteVariable { get; private set; }
    public static string WyamDeployBranchVariable { get; private set; }

    public static void SetVariableNames(
        string githubUserNameVariable = null,
        string githubPasswordVariable = null,
        string appVeyorApiTokenVariable = null,
        string wyamAccessTokenVariable = null,
        string wyamDeployRemoteVariable = null,
        string wyamDeployBranchVariable = null)
    {
        GithubUserNameVariable = githubUserNameVariable ?? "GITHUB_USERNAME";
        GithubPasswordVariable = githubPasswordVariable ?? "GITHUB_PASSWORD";
        AppVeyorApiTokenVariable = appVeyorApiTokenVariable ?? "APPVEYOR_API_TOKEN";
        WyamAccessTokenVariable = wyamAccessTokenVariable ?? "WYAM_ACCESS_TOKEN";
        WyamDeployRemoteVariable = wyamDeployRemoteVariable ?? "WYAM_DEPLOY_REMOTE";
        WyamDeployBranchVariable = wyamDeployBranchVariable ?? "WYAM_DEPLOY_BRANCH";
    }
}