using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy
{
    public class PipelineProgressUpdatedEventArgs : EventArgs
    {
        public PipelineProgressUpdatedEventArgs()
        {
            Id = Guid.NewGuid().ToString();
            TimeStamp = DateTime.UtcNow;
        }

        IDefaultQueueingPipelineTool SubjectPipelineTool { get; set; }
        /// <summary>
        /// a pipeline's progress is updated when a tool's progress is updated
        /// </summary>
        public PipelineToolProgressUpdatedEventArgs ToolProgressUpdatedEvent { get; internal set; }
        public string Id { get; private set; }
        public DateTime TimeStamp { get; private set; }

        // a pipeline's progress is updated when a tool completes
        public PipelineToolCompletedEventArgs ToolCompletedEvent { get; internal set; }

        // a pipeline's progress is updated when a tool starts
        public PipelineToolStartEventArgs ToolStartedEvent { get; internal set; }
    }
}
