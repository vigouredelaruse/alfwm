using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{
    public interface IFlowchartMergeNode : IFlowchartSequenceNode<IPipelineTool>
    {

        ICollection<IFlowchartSequenceNode<IPipelineTool>> InputNodes { get; set; }


        ICollection<IFlowchartSequenceNode<IPipelineTool>> OutputNodes { get; set; }

    }
}
