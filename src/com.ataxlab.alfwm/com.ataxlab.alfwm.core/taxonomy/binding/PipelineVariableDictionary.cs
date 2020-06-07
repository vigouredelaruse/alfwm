using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    /// <summary>
    /// provide indexer type symantics 
    /// 
    /// support expandable values of dictionary keys
    /// 
    /// a pipeline should be able to append values to key hits
    /// </summary>
    public class PipelineVariableDictionary :Dictionary<string, ICollection<PipelineVariable>>
    {
        public PipelineVariableDictionary() : base()
        { }
    }


}
