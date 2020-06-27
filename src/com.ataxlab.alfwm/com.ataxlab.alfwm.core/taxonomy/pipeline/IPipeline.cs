﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// base interface for a process definition ie pipeline
    /// specialization of process definition type is delegated
    /// to interfaces inheriting from this
    /// 
    /// resist the temptation to not prove the need
    /// to base this on IPipelineTool
    /// </summary>
    public interface IPipeline
    {
        string PipelineId { get; set; }
        string InstanceId { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
        event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        void OnPipelineStarted(object sender, PipelineStartedEventArgs args);

        event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);

        event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        void OnPipelineFailed(object sender, PipelineFailedEventArgs args);

        event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;
        void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
 
        IPipelineBinding InputBinding { get; set; }

        IPipelineBinding OutputBinding { get; set; }


        /// <summary>
        /// designed to permit implementers to supply their own
        /// Configuration and Pipeline types
        /// 
        /// since this is the initial entry point here one should
        /// construct a suitable context 
        /// </summary>
        /// <typeparam name="StartResult"></typeparam>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <returns></returns>
        StartResult Start<StartResult, StartConfiguration>(StartConfiguration configuration) 
            where StartConfiguration : class 
            where StartResult : class;

        StopResult Stop<StopResult>(string instanceId) where StopResult : class;


    }
   
    /// <summary>
    /// this is probably the least viable composition for an implementation
    /// of a pipeline
    /// </summary>
    /// <typeparam name="TProcessDefinition"></typeparam>
    public interface IPipeline<TProcessDefinition> : IPipeline
        where TProcessDefinition : class, new()
    {
        /// <summary>
        /// this collection will be determined by 
        /// the nature of your process definition 
        /// </summary>
        TProcessDefinition PipelineTools { get; set; }
    }
    public interface IPipelineObsolete
    {
        event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        void OnPipelineStarted(object sender, PipelineStartedEventArgs args);

        event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);

        event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        void OnPipelineFailed(object sender, PipelineFailedEventArgs args);

        event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;
        void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
        string InstanceId { get; set; }

        PipelineContext Context { get; set; }

        IPipelineBinding InputBinding { get; set; }

        IPipelineBinding OutputBinding { get; set; }

        IPipelineConfiguration Configuration { get; set; }

        /// <summary>
        /// designed to permit implementers to supply their own
        /// Configuration and Pipeline types
        /// 
        /// since this is the initial entry point here one should
        /// construct a suitable context 
        /// </summary>
        /// <typeparam name="StartResult"></typeparam>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <returns></returns>
        StartResult Start<StartResult, StartConfiguration>(StartConfiguration configuration) where StartConfiguration : IPipelineConfiguration where StartResult : IPipelineStatus;

        StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineStatus;


    }
}
