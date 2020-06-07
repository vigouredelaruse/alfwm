using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolProgressUpdatedEventArgs : EventArgs
    {
        public IPipelineToolStatus Status { get; set; }
        public IPipelineToolBinding OutputBinding { get; set; }
    }
}