using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{
    /// <summary>
    /// a linked list of specialized IPipelineTools
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public class QueueingPipelineProccessDefinition<TConfiguration>
    {
        LinkedList<IQueueingPipelineNode> QueueNodes { get; set; }
    }
}
