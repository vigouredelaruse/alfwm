using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IDefaultQueueingPipelineProcessDefinition
    {
        string Id { get; set; }

        LinkedList<DefaultQueueingPipelineToolNode> QueueingPipelineNodes { get; set; }


    }

 
}
