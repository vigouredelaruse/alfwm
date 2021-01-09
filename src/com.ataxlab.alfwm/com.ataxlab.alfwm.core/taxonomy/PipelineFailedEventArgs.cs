using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineFailedEventArgs : EventArgs
    {
       
        public PipelineFailedEventArgs()
        {
            Id = Guid.NewGuid().ToString();
            TimeStamp = DateTime.UtcNow;
        }

        public PipelineToolFailedEventArgs ToolFailedEvent { get;  set; }
        public string Id { get;  set; }
        public DateTime TimeStamp { get; set; }

        public Exception FailureException { get; set; }
    }
}
