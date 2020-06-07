using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    public abstract class ActivityContext : PipelineToolContext
    {
        public ActivityContext() : base()
        { }

        public ActivityContext(IPipelineToolConfiguration configuration) : base()
        { Configuration = configuration; }
    }
}
