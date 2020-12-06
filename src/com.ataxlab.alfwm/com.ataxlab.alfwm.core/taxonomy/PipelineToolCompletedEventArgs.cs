using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolCompletedEventArgs : EventArgs
    {
        public object Payload { get; set; }

        public string InstanceId { get; set; }

        public IPipelineToolStatus Status { get; set; }
    }

    public class PipelineToolCompletedEventArgs<TPayload> : EventArgs
        where TPayload : class
    {
        public PipelineToolCompletedEventArgs() { }

        public PipelineToolCompletedEventArgs(TPayload payload)
        {
            this.Payload = payload;
        }

        public TPayload Payload { get; set; }
    }
}
