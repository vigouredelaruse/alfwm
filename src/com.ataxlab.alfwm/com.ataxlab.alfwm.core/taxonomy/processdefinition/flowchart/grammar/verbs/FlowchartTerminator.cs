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
    public abstract class FlowchartTerminator : IFlowchartMergeNode
    {
        public abstract ICollection<IFlowchartSequenceNode<IPipelineTool>> InputNodes { get; set; }
        public abstract ICollection<IFlowchartSequenceNode<IPipelineTool>> OutputNodes { get; set; }
        public abstract string FlowChartSequenceNodeId { get; set; }
        public abstract EvaluateFlowchartNode InjectedNodeEvaluator { get; set; }
        public IPipelineTool PipelineTool { get; set; }

        protected virtual void EvaluateNode()
        {
            if(InjectedNodeEvaluator != null)
            {
                foreach(var registeredDelegate in InjectedNodeEvaluator.GetInvocationList())
                    registeredDelegate.Method?.Invoke(null,null);
            }
        }

        void IFlowchartSequenceNode<IPipelineTool>.EvaluateNode()
        {
            throw new NotImplementedException();
        }
    }
}
