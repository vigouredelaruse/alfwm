using com.ataxlab.alfwm.core.persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition.flowchart.grammar.verbs
{
    /// <summary>
    /// the abstract base supplies everything required
    /// decoration at this layer is probably a mistake
    /// 
    /// the real meat of the implementation will depend on 
    /// the persistence api, whether disk or network based
    /// </summary>
    public abstract class FlowchartDataSetProvider : PersistenceProvider
    {
    }
}
