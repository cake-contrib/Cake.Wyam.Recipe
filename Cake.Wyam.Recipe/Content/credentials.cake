public class GitHubCredentials
{
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public GitHubCredentials(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}

public class AppVeyorCredentials
{
    public string ApiToken { get; private set; }

    public AppVeyorCredentials(string apiToken)
    {
        ApiToken = apiToken;
    }
}

public class WyamCredentials
{
    public string AccessToken { get; private set; }
    public string DeployRemote { get; private set; }
    public string DeployBranch { get; private set; }

    public WyamCredentials(string accessToken, string deployRemote, string deployBranch)
    {
        AccessToken = accessToken;
        DeployRemote = deployRemote;
        DeployBranch = deployBranch;
    }
}

public class CloudflareCredentials
{
    public string AuthEmail { get; private set; }
    public string AuthKey { get; private set; }
    public string ZoneId { get; private set; }

    public CloudflareCredentials(string authEmail, string authKey, string zoneId)
    {
        AuthEmail = authEmail;
        AuthKey = authKey;
        ZoneId = zoneId;
    }
}

public static GitHubCredentials GetGitHubCredentials(ICakeContext context)
{
    return new GitHubCredentials(
        context.EnvironmentVariable(Environment.GithubUserNameVariable),
        context.EnvironmentVariable(Environment.GithubPasswordVariable));
}

public static AppVeyorCredentials GetAppVeyorCredentials(ICakeContext context)
{
    return new AppVeyorCredentials(
        context.EnvironmentVariable(Environment.AppVeyorApiTokenVariable));
}

public static WyamCredentials GetWyamCredentials(ICakeContext context)
{
    return new WyamCredentials(
        context.EnvironmentVariable(Environment.WyamAccessTokenVariable),
        context.EnvironmentVariable(Environment.WyamDeployRemoteVariable),
        context.EnvironmentVariable(Environment.WyamDeployBranchVariable));
}

public static CloudflareCredentials GetCloudflareCredentials(ICakeContext context)
{
    return new CloudflareCredentials(
        context.EnvironmentVariable(Environment.CloudflareAuthEmail),
        context.EnvironmentVariable(Environment.CloudflareAuthKey),
        context.EnvironmentVariable(Environment.CloudflareZoneId));
}