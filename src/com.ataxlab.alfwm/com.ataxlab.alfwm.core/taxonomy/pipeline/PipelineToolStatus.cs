using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    [Obsolete]
    public abstract class PipelineToolStatus : IPipelineToolStatus
    {
        public PipelineToolStatus()
        { }

        public virtual string StatusJson { get; set; }

        public virtual string StatusJsonSchema { get; set; }
    }
}
