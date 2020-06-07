using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolStartEventArgs : EventArgs
    {
        public string InstanceId { get; set; }

        public IPipelineToolStatus Status { get; set; }
    }
}
