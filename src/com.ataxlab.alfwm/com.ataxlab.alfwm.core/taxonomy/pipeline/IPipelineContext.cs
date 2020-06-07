using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IPipelineContext
    { 


    IPipelineConfiguration Configuration { get; set; }
    string DisplayName { get; set; }
    IPipelineBinding InputBinding { get; set; }
    DateTime MostRecentExecution { get; set; }
    DateTime MostRecentScheduled { get; set; }
    IPipelineBinding OutputBinding { get; set; }
    IPipelineStatus Status { get; set; }
}
}
