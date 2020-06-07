using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// designed for generalized reporting of status
    /// and supply of configuration
    /// each tuple provides a json schema
    /// 
    /// resist the temptation to not prove
    /// the need to base other interfaces on this
    /// </summary>
    public interface IPipelineTool
    {
        string InstanceId { get; set; }

        event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);

        event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);

        event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);

        event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;
        void OnPipelineToolCompleted(object sender, PipelineToolCompletedEventArgs args);


        IPipelineToolStatus Status { get; set; }
        IPipelineToolContext Context { get; set; }
        IPipelineToolConfiguration Configuration { get; set; }

        IPipelineToolBinding OutputBinding { get; set; }
        void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Action<StartResult> callback) where StartConfiguration : IPipelineToolConfiguration, new() where StartResult : IPipelineToolStatus, new();
        void Start<StartResult>(Action<StartResult> callback) where StartResult : IPipelineToolStatus, new();

        StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();

    }
}
