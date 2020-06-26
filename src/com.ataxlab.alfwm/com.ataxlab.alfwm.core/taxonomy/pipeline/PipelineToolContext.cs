using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    [Obsolete]
    public abstract class PipelineToolContext : IPipelineToolContext
    {
        public virtual string DisplayName { get; set; }

        public virtual IPipelineToolConfiguration Configuration { get; set; }

        public virtual DateTime MostRecentScheduled { get; set; }

        public virtual DateTime MostRecentExecution { get; set; }

 
        public virtual IPipelineToolBinding InputBinding { get; set; }

        public virtual IPipelineToolBinding OutputBinding { get; set; }

        public virtual IPipelineToolStatus PipelineToolStatus { get;  set; }
    }
}
