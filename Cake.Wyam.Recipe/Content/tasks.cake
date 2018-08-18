public class BuildTasks
{
    public CakeTaskBuilder PrintAppVeyorEnvironmentVariablesTask { get; set; }
    public CakeTaskBuilder ClearAppVeyorCacheTask { get; set; }
    public CakeTaskBuilder ShowInfoTask { get; set; }
    public CakeTaskBuilder DefaultTask { get; set; }
    public CakeTaskBuilder AppVeyorTask { get; set; }
    public CakeTaskBuilder ClearCacheTask { get; set; }
    public CakeTaskBuilder PreviewTask { get; set; }
    public CakeTaskBuilder CleanDocumentationTask { get; set; }
    public CakeTaskBuilder BuildDocumentationTask { get; set; }
    public CakeTaskBuilder PublishDocumentationTask { get; set; }
    public CakeTaskBuilder PurgeCloudflareCacheTask { get; set; }
    public CakeTaskBuilder PreviewDocumentationTask { get; set; }
}
