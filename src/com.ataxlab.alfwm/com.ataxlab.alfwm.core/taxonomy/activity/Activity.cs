using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    public abstract class Activity : IPipelineTool
    {
        public abstract string InstanceId { get; set; }
        public abstract IPipelineToolStatus Status { get; set; }
        public abstract IPipelineToolContext Context { get; set; }
        public abstract IPipelineToolConfiguration Configuration { get; set; }
        public abstract IPipelineToolBinding OutputBinding { get; set; }

        public virtual event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public virtual event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public virtual event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public virtual event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public virtual void OnPipelineToolCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = PipelineToolCompleted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            EventHandler<PipelineToolFailedEventArgs> handler = PipelineToolFailed;
            if(handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            EventHandler<PipelineToolProgressUpdatedEventArgs> handler = PipelineToolProgressUpdated;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            EventHandler<PipelineToolStartEventArgs> handler = PipelineToolStarted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }
        public abstract void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Action<StartResult> callback)
            where StartResult : IPipelineToolStatus, new()
            where StartConfiguration : IPipelineToolConfiguration, new();
        public abstract void Start<StartResult>(Action<StartResult> callback) where StartResult : IPipelineToolStatus, new();
        public abstract StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
