using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    [Obsolete]
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

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class;

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

        public abstract void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
            where StartResult : class, new()
            where StartConfiguration : class, new();
        public abstract void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class;
        public abstract StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
