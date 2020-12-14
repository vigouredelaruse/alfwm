using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class QueueingPipelineToolConfiguration : IPipelineToolConfiguration
    {
        public string Id { get; set;}
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public object Configuration { get; set; }
        public DateTime DeploymentTime { get; set; }
        public string ConfigurationJson { get; set; }
        public string ConfigurationJsonSchema { get; set; }
    }
}
