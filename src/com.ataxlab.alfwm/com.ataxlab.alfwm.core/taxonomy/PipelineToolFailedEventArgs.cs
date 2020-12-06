using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolFailedEventArgs : EventArgs
    {
        public string InstanceId { get; set; }

        public IPipelineToolStatus Status { get; set; }
    }
}