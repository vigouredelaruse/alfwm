using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolStartEventArgs : EventArgs
    {
        public PipelineToolStartEventArgs()
        {
            this.EventId = Guid.NewGuid().ToString();
            this.EventTimeStamp = DateTime.UtcNow;
        }

        public String PipelineToolDisplayName { get; set; }
        public string InstanceId { get; set; }

        public IPipelineToolStatus Status { get; set; }
        public string EventId { get; private set; }
        public DateTime EventTimeStamp { get; private set; }
    }
}
