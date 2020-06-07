using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{

    public delegate void EvaluateFlowchartNode();

    /// <summary>
    /// decorated marker interface -
    /// parsing a flowchart sequence requires
    /// specialized nodes and we cannot ignore
    /// the eulerian graphiness of a flowchart
    /// node we are using to specify a pipeline step
    /// </summary>
    public interface IFlowchartSequenceNode<TFlowchartNode> where TFlowchartNode : IPipelineTool
    {
        string FlowChartSequenceNodeId { get; set; }

        /// <summary>
        /// entry point for logic executed by the node
        /// </summary>
        EvaluateFlowchartNode ProcessNodeEvaulator { get; set; }

        /// <summary>
        /// canonical parameterless evaluator
        /// 
        /// can take advantage of ProcessNodeEvaluator for 
        /// injected processing
        /// </summary>
        void EvaluateNode();


    }




}
