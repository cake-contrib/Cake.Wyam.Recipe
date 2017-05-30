public class BuildPaths
{
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

        var buildDirectories = new BuildDirectories(
            buildDirectoryPath,
            publishedDocumentationDirectory
            );

        return new BuildPaths
        {
            Directories = buildDirectories
        };
    }
}

public class BuildDirectories
{
    public DirectoryPath Build { get; private set; }
    public DirectoryPath PublishedDocumentation { get; private set; }

    public BuildDirectories(
        DirectoryPath build,
        DirectoryPath publishedDocumentation
        )
    {
        Build = build;
        PublishedDocumentation = publishedDocumentation;
    }
}