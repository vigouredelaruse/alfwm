using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public abstract class PipelineConfiguration : IPipelineConfiguration
    {
        public virtual string DisplayName { get; set; }
        public virtual  DateTime DeploymentTime { get; set; }
        public virtual string ConfigurationJson { get; set; }
        public virtual string ConfigurationJsonSchema { get; set; }
    }
}
