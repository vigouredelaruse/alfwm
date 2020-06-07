using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.workflow
{
    public abstract class WorkflowContext : PipelineToolContext
    {
        public WorkflowContext() : base()
        { }

        public WorkflowContext(WorkflowConfiguration configuration) : base()
        { Configuration = configuration; }

    }
}
