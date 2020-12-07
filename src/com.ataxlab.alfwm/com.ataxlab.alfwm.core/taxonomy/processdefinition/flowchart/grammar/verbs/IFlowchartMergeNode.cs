using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{
    public interface IFlowchartMergeNode<TFlowchartNode, TConfiguration> : IFlowchartSequenceNode<TFlowchartNode, TConfiguration>
        where TConfiguration : class, new()
        where TFlowchartNode : class, IPipelineTool<TConfiguration>, new()
    {

        ICollection<TFlowchartNode> InputNodes { get; set; }


        ICollection<TFlowchartNode> OutputNodes { get; set; }

    }
}
