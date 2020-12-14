using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineStartedEventArgs : EventArgs
    {
        public PipelineStartedEventArgs() : base()
        {
            EventId = Guid.NewGuid().ToString();
            EventTimeStamp = DateTime.UtcNow;
        }

        public PipelineToolStartEventArgs SourceEvent { get; set; }
        public String EventId { get; set; }

        public String PipelineId { get; set; }

        public String PipelineDisplayName { get; set; }

        /// <summary>
        /// cardinal node sequence id im the tool chain
        /// </summary>
        public int StartedPipelineCardinalNode { get; set; }

        public String StartedPipelineNodeId { get; set; }

        public DateTime EventTimeStamp { get; set; }
    }
}
