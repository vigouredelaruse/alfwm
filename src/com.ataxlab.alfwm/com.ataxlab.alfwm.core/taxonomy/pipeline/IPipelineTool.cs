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

        /// <summary>
        /// support reporting completion with a payload
        /// </summary>
        /// <typeparam name="TPayload"></typeparam>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class;


        IPipelineToolStatus Status { get; set; }
        IPipelineToolContext Context { get; set; }
        IPipelineToolConfiguration Configuration { get; set; }

        IPipelineToolBinding OutputBinding { get; set; }

        /// <summary>
        /// a start method for a delegate that has a user specified output
        /// an implementation may choose to for instance, pass the output
        /// to new activities
        /// </summary>
        /// <typeparam name="StartResult"></typeparam>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="callback"></param>
        void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback) 
            where StartConfiguration : class, new() 
            where StartResult : class, new();
        

        /// <summary>
        /// a start method for a delegate that has no output
        /// </summary>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="callback"></param>
        void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) 
            where StartConfiguration : class;

        StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();

    }
}
