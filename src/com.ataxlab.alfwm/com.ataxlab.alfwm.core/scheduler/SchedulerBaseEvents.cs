using com.ataxlab.alfwm.core.taxonomy;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.scheduler
{
    public abstract partial class SchedulerBase : IScheduler
    {
        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineToolStartEventArgs> WorkflowStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> WorkflowProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> WorkflowFailed;
        public event EventHandler<PipelineToolStartEventArgs> ActivityStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> ActivityProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> ActivityFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> WorkflowCompleted;
        public event EventHandler<PipelineToolCompletedEventArgs> ActivityCompleted;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public virtual void OnActivityCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = ActivityCompleted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnActivityFailed(object sender, PipelineToolFailedEventArgs args)
        {
            EventHandler<PipelineToolFailedEventArgs> handler = ActivityFailed;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnActivityProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            EventHandler<PipelineToolProgressUpdatedEventArgs> handler = ActivityProgressUpdated;
            if (handler != null)
            {
                handler(sender, args);

            }
        }

        public virtual void OnActivityStarted(object sender, PipelineToolStartEventArgs args)
        {
            EventHandler<PipelineToolStartEventArgs> handler = ActivityStarted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

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

        public virtual void OnWorkflowCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = WorkflowCompleted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnWorkflowFailed(object sender, PipelineToolFailedEventArgs args)
        {
            EventHandler<PipelineToolFailedEventArgs> handler = WorkflowFailed;
            if (handler != null)
            {
                handler(sender, args);

            }
        }

        public virtual void OnWorkflowProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            EventHandler<PipelineToolProgressUpdatedEventArgs> handler = WorkflowProgressUpdated;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public virtual void OnWorkflowStarted(object sender, PipelineToolStartEventArgs args)
        {
            EventHandler<PipelineToolStartEventArgs> handler = WorkflowStarted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}
