using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{
    /// <summary>
    /// supports choosing an output node based on some input
    /// that presumably contains a collection of types
    /// that contain output nodes as metadata
    /// 
    /// TInputNodes & TOutputNodes deliberately vague
    /// though the expectation is that they are valid
    /// types for the flowchart process definition model
    /// 
    /// at minimum TInputNodes & TOutputNodes must support
    /// identifying the output pipeline tool
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TSelectedItem"></typeparam>
    public interface IFlowchartChooseOnePredicateSequenceNode<TInput, TSelectedItem, TInputNodes, TOutputNode, TFlowchartNode, TConfiguration> : 
        IFlowchartSequenceNode<TFlowchartNode, TConfiguration> 
        where TSelectedItem : TFlowchartNode
        where TConfiguration : class, new()
        where TFlowchartNode : IPipelineTool<TConfiguration>
    {
        Func<TInput, TSelectedItem> ChoicePredicate { get; set; }

        TInputNodes InputNodes { get; set; }

        TOutputNode OutputNode { get; set; }
    }
}
