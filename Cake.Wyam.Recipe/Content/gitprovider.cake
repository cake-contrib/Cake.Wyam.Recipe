/// <summary>
/// Supported ways to interact with Git Repository.
/// </summary>
public enum GitProviderType
{
    /// <summary>
    /// Interact with Git though Cake.Git addin.
    /// Requires system to be compatible with Cake.Git addin.
    /// </summary>
    CakeGit,
    /// <summary>
    /// Interact with Git though Cake.Git using Git CLI.
    /// Requires Git CLI to be available in path.
    /// </summary>
    Cli
}

/// <summary>
/// Description of a provider to work with a Git repository.
/// </summary>
public interface IGitProvider
{
    /// <summary>
    /// Clones a Git repository.
    /// </summary>
    /// <param name="context">The Cake context.</param>
    /// <param name="sourceUrl">URL of the repository to clone.</param>
    /// <param name="workDirectoryPath">Path where the repository should be cloned to.</param>
    /// <param name="branchName">Name of the branch to clone.</param>
    void Clone(ICakeContext context, string sourceUrl, DirectoryPath workDirectoryPath, string branchName);

    /// <summary>
    /// Returns the the latest commit.
    /// </summary>
    /// <param name="context">The Cake context.</param>
    /// <param name="repositoryDirectoryPath">Root directory of the repository.</param>
    /// <returns>Information about the latest commit</returns>
    (string Sha, string Name, string Email, string Message) GetCommit(ICakeContext context, DirectoryPath repositoryRootDirectory);

    /// <summary>
    /// Commits changes in a repository.
    /// </summary>
    /// <param name="context">The Cake context.</param>
    /// <param name="repositoryDirectoryPath">Root directory of the repository.</param>
    /// <param name="name">Name of the committer.</param>
    /// <param name="email">Email address of the committer.</param>
    /// <param name="message">Commit message.</param>
    void Commit(ICakeContext context, DirectoryPath repositoryDirectoryPath, string name, string email, string message);

    /// <summary>
    /// Checks if there are uncommitted changes in a repository.
    /// </summary>
    /// <param name="context">The Cake context.</param>
    /// <param name="repositoryDirectoryPath">Root directory of the repository.</param>
    void HasUncommitedChanges(ICakeContext context, DirectoryPath repositoryDirectoryPath);

    /// <summary>
    /// Pushes changes to remote repository.
    /// </summary>
    /// <param name="context">The Cake context.</param>
    /// <param name="repositoryDirectoryPath">Root directory of the repository.</param>
    /// <param name="username">Username used for authentication.</param>
    /// <param name="password">Password used for authentication.</param>
    /// <param name="branchName">Name of branch to push.</param>
    void Push(ICakeContext context, DirectoryPath repositoryDirectoryPath, string username, string password, string branchName);
}

/// <summary>
/// Provider to interact with a repository using Git CLI.
/// </summary>
public class CliGitProvider : IGitProvider
{
    /// <inheritdoc />
    void Clone(ICakeContext context, string sourceUrl, DirectoryPath workDirectoryPath, string branchName);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.EnsureDirectoryExists(workDirectoryPath);

        this.GitCommand(
            context,
            workDirectoryPath,
            "clone",
            "--branch {branchName}",
            sourceUrl);
    }

    /// <inheritdoc />
    (string Sha, string Name, string Email, string Message) GetCommit(ICakeContext context, DirectoryPath repositoryRootDirectory);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (repositoryDirectoryPath == null)
        {
            throw new ArgumentNullException(nameof(repositoryDirectoryPath));
        }

        // TODO
        // var commit = context.GitLogTip(repositoryRootDirectory);

        return (commit.Sha, commit.Committer.Name, commit.Committer.Email, commit.Committer.Message)
    }


    /// <inheritdoc />
    public void Commit(ICakeContext context, DirectoryPath repositoryDirectoryPath, string name, string email, string message)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (repositoryDirectoryPath == null)
        {
            throw new ArgumentNullException(nameof(repositoryDirectoryPath));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentNullException(nameof(message));
        }

        this.GitCommand(
            context,
            repositoryDirectoryPath,
            "commit",
            "--author",
            $"{name} <{email}>",
            "--message",
            $"\"{message}\"");
    }

    /// <inheritdoc />
    void HasUncommitedChanges(ICakeContext context, DirectoryPath repositoryDirectoryPath);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // TODO
    }

    /// <inheritdoc />
    void Push(ICakeContext context, DirectoryPath repositoryDirectoryPath, string username, string password, string branchName);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // TODO
    }

    private static IEnumerable<string> GitCommand(
        ICakeContext context,
        DirectoryPath repositoryDirectoryPath,
        params string[] arguments)
    {
        if (!arguments.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(arguments));
        }
        var gitArguments = string.Join(" ", arguments);
        var exitCode = context.StartProcess(
            "git",
            new ProcessSettings
            {
                Arguments = gitArguments,
                WorkingDirectory = repositoryDirectoryPath.FullPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            },
            out var redirectedStandardOutput,
            out var redirectedErrorOutput
        );
        if (exitCode != 0)
        {
            throw new Exception(
                $"Git command failed with arguments {gitArguments}. Exit code: {exitCode}. Error output: {string.Join(System.Environment.NewLine, redirectedErrorOutput)}"
            );
        }
        return redirectedStandardOutput;
    }
}

/// <summary>
/// Provider to interact with a repository using Cake.Git addin.
/// </summary>
public class CakeGitProvider : IGitProvider
{
    /// <inheritdoc />
    void Clone(ICakeContext context, string sourceUrl, DirectoryPath workDirectoryPath, string branchName);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.GitClone(remote, workDirectoryPath, new GitCloneSettings{ BranchName = branchName });
    }

    /// <inheritdoc />
    (string Sha, string Name, string Email, string Message) GetCommit(ICakeContext context, DirectoryPath repositoryRootDirectory);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (repositoryDirectoryPath == null)
        {
            throw new ArgumentNullException(nameof(repositoryDirectoryPath));
        }

        var commit = context.GitLogTip(repositoryRootDirectory);

        return (commit.Sha, commit.Committer.Name, commit.Committer.Email, commit.Committer.Message)
    }

    /// <inheritdoc />
    public void Commit(ICakeContext context, DirectoryPath repositoryDirectoryPath, string name, string email, string message)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (repositoryDirectoryPath == null)
        {
            throw new ArgumentNullException(nameof(repositoryDirectoryPath));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentNullException(nameof(message));
        }

        context.GitCommit(repositoryDirectoryPath, name, email, message);
    }

    /// <inheritdoc />
    void HasUncommitedChanges(ICakeContext context, DirectoryPath repositoryDirectoryPath);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.GitHasUncommitedChanges(repositoryDirectoryPath);
    }

    /// <inheritdoc />
    void Push(ICakeContext context, DirectoryPath repositoryDirectoryPath, string username, string password, string branchName);
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.GitPush(repositoryDirectoryPath, username, password, branchName);
    }
}