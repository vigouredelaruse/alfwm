using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.workflow
{
    public abstract class SequenceWorkflow<TSequenceWorkflowConfiguration> : IPipelineTool<TSequenceWorkflowConfiguration>
        where TSequenceWorkflowConfiguration : class, new()
    {

        
        public virtual string PipelineToolInstanceId { get; set; }
        public virtual IPipelineToolStatus PipelineToolStatus { get; set; }
        public virtual IPipelineToolContext PipelineToolContext { get; set; }
        public virtual IPipelineToolConfiguration<TSequenceWorkflowConfiguration> PipelineToolConfiguration { get; set; }
        public virtual IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get ; set; }

        public virtual event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public virtual event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public virtual event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public virtual void OnPipelineToolCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = PipelineToolCompleted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();

        public virtual void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            EventHandler<PipelineToolFailedEventArgs> handler = PipelineToolFailed;
            if (handler != null)
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

        public abstract void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
            where StartConfiguration : class, IPipelineToolConfiguration, new();
        public abstract void StartPipelineTool(TSequenceWorkflowConfiguration configuration, Action<TSequenceWorkflowConfiguration> callback);
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
