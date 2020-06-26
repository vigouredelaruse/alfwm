using com.ataxlab.alfwm.core.taxonomy.binding;
using System;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    [Obsolete]
    public interface IPipelineToolContext
    {
        IPipelineToolConfiguration Configuration { get; set; }
        string DisplayName { get; set; }
        IPipelineToolBinding InputBinding { get; set; }
        DateTime MostRecentExecution { get; set; }
        DateTime MostRecentScheduled { get; set; }
        IPipelineToolBinding OutputBinding { get; set; }
        IPipelineToolStatus PipelineToolStatus { get; set; }
    }
}