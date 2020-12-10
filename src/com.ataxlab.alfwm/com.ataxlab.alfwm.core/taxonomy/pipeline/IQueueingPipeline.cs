using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
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
        bool Bind<TInputQEntity, TOutputQEntity>(string SourceInstanceId, string DestinationInstanceId);
    }
}
