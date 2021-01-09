using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class PipelineDeploymentFailedEventArgs : EventArgs
    {
        public PipelineDeploymentFailedEventArgs()
        {
            this.Id = Guid.NewGuid().ToString();
            this.TimeStamp = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public Exception DeploymentFailureException { get; set; }
    }
}
