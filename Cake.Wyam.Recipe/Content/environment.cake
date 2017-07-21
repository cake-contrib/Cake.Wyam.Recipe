public static class Environment
{
    public static string GithubUserNameVariable { get; private set; }
    public static string GithubPasswordVariable { get; private set; }
    public static string AppVeyorApiTokenVariable { get; private set; }
    public static string WyamAccessTokenVariable { get; private set; }
    public static string WyamDeployRemoteVariable { get; private set; }
    public static string WyamDeployBranchVariable { get; private set; }
    public static string CloudflareAuthEmail { get; private set; }
    public static string CloudflareAuthKey { get; private set; }
    public static string CloudflareZoneId { get; private set; }

    public static void SetVariableNames(
        string githubUserNameVariable = null,
        string githubPasswordVariable = null,
        string appVeyorApiTokenVariable = null,
        string wyamAccessTokenVariable = null,
        string wyamDeployRemoteVariable = null,
        string wyamDeployBranchVariable = null,
        string cloudflareAuthEmail = null,
        string cloudflareAuthKey = null,
        string cloudflareZoneId = null)
    {
        GithubUserNameVariable = githubUserNameVariable ?? "GITHUB_USERNAME";
        GithubPasswordVariable = githubPasswordVariable ?? "GITHUB_PASSWORD";
        AppVeyorApiTokenVariable = appVeyorApiTokenVariable ?? "APPVEYOR_API_TOKEN";
        WyamAccessTokenVariable = wyamAccessTokenVariable ?? "WYAM_ACCESS_TOKEN";
        WyamDeployRemoteVariable = wyamDeployRemoteVariable ?? "WYAM_DEPLOY_REMOTE";
        WyamDeployBranchVariable = wyamDeployBranchVariable ?? "WYAM_DEPLOY_BRANCH";
        CloudflareAuthEmail = cloudflareAuthEmail ?? "CLOUDFLARE_AUTH_EMAIL";
        CloudflareAuthKey = cloudflareAuthKey ?? "CLOUDFLARE_AUTH_KEY";
        CloudflareZoneId = cloudflareZoneId ?? "CLOUDFLARE_ZONE_ID";
    }
}