using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    
    public class QueueingPipelineProccessDefinition<TConfiguration>
    {
        LinkedList<IQueueingPipelineNode> QueueNodes { get; set; }
    }
}
