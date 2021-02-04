using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineCompletedEventArgs : EventArgs
    {
        public PipelineCompletedEventArgs()
        {
            TimeStamp = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public DateTime TimeStamp { get; set; }
        public String Id { get; set; }
        public PipelineToolCompletedEventArgs Payload { get; set; }
    }
}
