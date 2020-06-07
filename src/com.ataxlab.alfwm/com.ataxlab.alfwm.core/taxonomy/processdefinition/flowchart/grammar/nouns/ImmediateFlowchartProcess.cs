using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.nouns
{
    /// <summary>
    /// as opposed to a background or timer triggered scheduled process
    /// </summary>
    public abstract class ImmediateFlowchartProcess : IFlowchartSequenceNode<IPipelineTool>
    {
        public abstract string FlowChartSequenceNodeId { get; set; }
        public abstract EvaluateFlowchartNode InjectedNodeEvaluator { get; set; }

        /// <summary>
        /// eligible for scheduling
        /// 
        /// due to separation of concerns
        /// this tool can be nominated for scheduling
        /// folling execution of EvaulateNode()
        /// </summary>
        public abstract IPipelineTool PipelineTool { get; set; }

        /// <summary>
        /// the evaulator that nominates the associated pipeline tool
        /// for scheduling
        /// </summary>
        public abstract void EvaluateNode();
    }
}
