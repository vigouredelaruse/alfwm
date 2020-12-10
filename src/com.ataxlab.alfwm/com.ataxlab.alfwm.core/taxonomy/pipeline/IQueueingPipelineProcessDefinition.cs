using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IQueueingPipelineProcessDefinition
    {
        string Id { get; set; }

        /// <summary>
        /// the Pipeline's toolchain
        /// indexed by InstanceId
        /// 
        /// the payload is expected (by the Bind mechanism)
        /// to expose a ConcurrentQueue<Entity>         /// </summary>
        ConcurrentDictionary<string, IQueueingPipelineNode> PipelineToolChain { get; set; }
    }
}
