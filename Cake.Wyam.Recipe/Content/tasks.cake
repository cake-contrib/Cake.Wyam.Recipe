public class BuildTasks
{
    public CakeTaskBuilder<ActionTask> PrintAppVeyorEnvironmentVariablesTask { get; set; }
    public CakeTaskBuilder<ActionTask> ClearAppVeyorCacheTask { get; set; }
    public CakeTaskBuilder<ActionTask> ShowInfoTask { get; set; }
    public CakeTaskBuilder<ActionTask> DefaultTask { get; set; }
    public CakeTaskBuilder<ActionTask> AppVeyorTask { get; set; }
    public CakeTaskBuilder<ActionTask> ClearCacheTask { get; set; }
    public CakeTaskBuilder<ActionTask> PreviewTask { get; set; }
    public CakeTaskBuilder<ActionTask> CleanDocumentationTask { get; set; }
    public CakeTaskBuilder<ActionTask> BuildDocumentationTask { get; set; }
    public CakeTaskBuilder<ActionTask> PublishDocumentationTask { get; set; }
    public CakeTaskBuilder<ActionTask> PurgeCloudflareCacheTask { get; set; }
    public CakeTaskBuilder<ActionTask> PreviewDocumentationTask { get; set; }
}
