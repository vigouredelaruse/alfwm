using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public abstract class PipelineContext : IPipelineContext
    {
        public PipelineContext()
        { }

        public string DisplayName { get; set; }

        public virtual IPipelineStatus Status { get; set; }

        public virtual DateTime MostRecentScheduled { get; set; }

        public virtual DateTime MostRecentExecution { get; set; }

        public virtual IPipelineConfiguration Configuration { get; set; }

        public virtual IPipelineBinding InputBinding { get; set; }

        public virtual IPipelineBinding OutputBinding { get; set; }
    }
}
