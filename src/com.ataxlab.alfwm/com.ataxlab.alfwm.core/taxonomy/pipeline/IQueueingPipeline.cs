using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// furnish a queueing specialization of 
    /// the pipeline interface
    /// </summary>
    public interface IQueueingPipeline : IPipeline<IQueueingPipelineProcessDefinition>
    {
        /// <summary>
        /// wire the output of the source queue
        /// to the input of the destination queue
        /// </summary>
        /// <typeparam name="TInputQEntity"></typeparam>
        /// <typeparam name="TOutputQEntity"></typeparam>
        /// <param name="SourceInstanceId"></param>
        /// <param name="DestinationInstanceId"></param>
        /// <returns></returns>
        bool Bind(string SourceInstanceId, string DestinationInstanceId);
    }
}
