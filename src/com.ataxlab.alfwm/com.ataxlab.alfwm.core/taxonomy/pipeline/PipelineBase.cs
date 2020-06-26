using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// convenience implementation of IPipeline 
    /// so implementors don't have to begin from the bare
    /// interface specification
    /// </summary>
    [Obsolete]
    public abstract class PipelineBase : IPipeline
    {
        public PipelineBase()
        {
        }

        public PipelineBase(IPipelineConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        public virtual IPipelineConfiguration Configuration { get; set; }

        public virtual PipelineContext Context { get; set; }

        public  string InstanceId { get; set; }
        public virtual IPipelineBinding InputBinding { get; set; }
        public virtual IPipelineBinding OutputBinding { get; set; }

        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public virtual void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            EventHandler<PipelineCompletedEventArgs> handler = PipelineCompleted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }
        public virtual void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            EventHandler<PipelineFailedEventArgs> handler = PipelineFailed;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            EventHandler<PipelineProgressUpdatedEventArgs> handler = PipelineProgressUpdated;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            EventHandler<PipelineStartedEventArgs> handler = PipelineStarted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public abstract StartResult Start<StartResult, StartConfiguration>(StartConfiguration configuration) where StartConfiguration : IPipelineConfiguration where StartResult : IPipelineStatus;

        public abstract StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineStatus;
    }
}
