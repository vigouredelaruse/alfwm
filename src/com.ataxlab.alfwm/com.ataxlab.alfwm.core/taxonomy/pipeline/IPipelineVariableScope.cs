using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// append this interface to your IPipeline and IPipelineTool 
    /// implementations to get scoped variables
    /// 
    /// you need to handle polymorphic collections in some clevr way
    /// 
    /// for instance, you could define some types with a common property
    /// (such as System.Collectoins.ICollection
    /// and make TPipelineVariables a kine of the common property
    /// </summary>
    public interface IPipelineVariableScope<TPipelineVariables>
    {
        TPipelineVariables Variables { get; set; }
    }
}
