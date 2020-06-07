using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{
    /// <summary>
    /// support true/false decision flowchart node
    /// exposes a delegate for user supplied decision logic
    /// </summary>
    /// <typeparam name="TLeftInput"></typeparam>
    /// <typeparam name="TRightInput"></typeparam>
    /// <typeparam name="TPredicate"></typeparam>
    public interface IFlowchartBinaryPredicateSequenceNode<TLeftInput, TRightInput> : IFlowchartSequenceNode<IPipelineTool>
    {
        Func<TLeftInput, TRightInput, bool> BinaryPredicate { get; set; }
    }
}
