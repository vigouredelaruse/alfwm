using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class PipelineToolConfiguration<TPayload> : IPipelineToolConfiguration<TPayload>
        where TPayload : class, new()
    {
        public TPayload Configuration { get; set;}
        public string DisplayName { get; set;}
        public DateTime DeploymentTime { get; set;}
        public string ConfigurationJson { get; set;}
        public string ConfigurationJsonSchema { get; set;}
    }

}
