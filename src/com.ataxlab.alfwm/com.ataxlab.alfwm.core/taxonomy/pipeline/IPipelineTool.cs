using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public interface IPipelineTool<TConfiguration>
        // where TConfiguration : IPipelineToolConfiguration
        //where TConfiguration : class, new()
    {

        IPipelineToolConfiguration<TConfiguration> PipelineToolConfiguration { get; set; }
        /// <summary>
        /// transient machine readable id
        /// </summary>
        string PipelineToolInstanceId { get; set; }

        ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }

        /// <summary>
        /// static machine readable id
        /// </summary>
        string PipelineToolId { get; set; }
        string PipelineToolDisplayName { get; set; }
        string PipelineToolDescription { get; set; }
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
        void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();


        IPipelineToolStatus PipelineToolStatus { get; set; }
        IPipelineToolContext PipelineToolContext { get; set; }

        IPipelineToolBinding PipelineToolOutputBinding { get; set; }

        /// <summary>
        /// start with the Class Generic parameter 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="callback"></param>
        void StartPipelineTool(TConfiguration configuration, Action<TConfiguration> callback);

        ///// <summary>
        ///// a start method for a delegate that has no output
        ///// </summary>
        ///// <typeparam name="StartConfiguration"></typeparam>
        ///// <param name="configuration"></param>
        ///// <param name="callback"></param>
        //void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) 
        //    where StartConfiguration : class, IPipelineToolConfiguration<TConfiguration>, new();

        StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();

    }
}
