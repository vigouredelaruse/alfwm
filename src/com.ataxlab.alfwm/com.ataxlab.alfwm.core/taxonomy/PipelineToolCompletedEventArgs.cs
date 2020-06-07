using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineToolCompletedEventArgs : EventArgs
    {
        public string Payload { get; set; }
    }
}
