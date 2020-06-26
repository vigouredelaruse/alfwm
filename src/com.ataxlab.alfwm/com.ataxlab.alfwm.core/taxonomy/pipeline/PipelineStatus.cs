using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// designed for implementers to conveniently create their own
    /// xxxPipelineStatus
    /// 
    /// and 
    /// 
    /// inherit their own arbitrary self-describing Status object
    /// </summary>
    [Obsolete]
    public abstract class PipelineStatus : IPipelineStatus
    {
        public PipelineStatus()
        { }


        public virtual string StatusJson { get; set; }
        public virtual string StatusJsonSchema { get; set; }
    }
}
