public class BuildPaths
{
    public BuildFiles Files { get; private set; }
    public BuildDirectories Directories { get; private set; }

    public static BuildPaths GetPaths(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        // Directories
        var buildDirectoryPath             = "./BuildArtifacts";
        var publishedDocumentationDirectory= buildDirectoryPath + "/Documentation";

        // Files
        var repoFilesPaths = new FilePath[] {
            "LICENSE",
            "README.md"
        };

        var buildDirectories = new BuildDirectories(
            buildDirectoryPath,
            publishedDocumentationDirectory,
            );

        var buildFiles = new BuildFiles(
            context,
            repoFilesPaths
            );

        return new BuildPaths
        {
            Files = buildFiles,
            Directories = buildDirectories
        };
    }
}

public class BuildFiles
{
    public ICollection<FilePath> RepoFilesPaths { get; private set; }

    public BuildFiles(
        ICakeContext context,
        FilePath[] repoFilesPaths
        )
    {
        RepoFilesPaths = Filter(context, repoFilesPaths);
    }
}

public class BuildDirectories
{
    public DirectoryPath Build { get; private set; }
    public DirectoryPath PublishedDocumentation { get; private set; }
    public ICollection<DirectoryPath> ToClean { get; private set; }

    public BuildDirectories(
        DirectoryPath build,
        DirectoryPath publishedDocumentation
        )
    {
        Build = build;
        PublishedDocumentation = publishedDocumentation;

        ToClean = new[] {
            Build
        };
    }
}