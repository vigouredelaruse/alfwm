using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{
    /// <summary>
    /// supports termination with output, ie scoped termination
    /// 
    /// output node population is independent of any other concern
    /// </summary>
    public abstract class FlowchartTerminator<TPipelineTool, TConfiguration> : IFlowchartMergeNode<TPipelineTool, TConfiguration>
        where TConfiguration : class, new()
        where TPipelineTool : class, IPipelineTool<TConfiguration>, new()
    {
        public abstract ICollection<TPipelineTool> InputNodes { get; set; }
        public abstract ICollection<TPipelineTool> OutputNodes { get; set; }
        public abstract string FlowChartSequenceNodeId { get; set; }
        public abstract EvaluateFlowchartNode InjectedNodeEvaluator { get; set; }
        public TPipelineTool PipelineTool { get; set; }
 
        public virtual void EvaluateNode()
        {
            if(InjectedNodeEvaluator != null)
            {
                foreach(var registeredDelegate in InjectedNodeEvaluator.GetInvocationList())
                    registeredDelegate.Method?.Invoke(null,null);
            }
        }

    }
}
