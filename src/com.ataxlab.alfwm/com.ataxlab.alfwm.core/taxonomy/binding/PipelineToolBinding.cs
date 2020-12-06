using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    /// <summary>
    /// designed for ease of use lookup in 
    /// dictionary based collections
    /// </summary>
    public abstract class PipelineToolBinding : IPipelineToolBinding
    {
        public PipelineToolBinding() 
        { }

        public virtual string PipelineToolBindingDisplayName { get; set; }
        public virtual string PipelineToolBindingKey { get; set; }
        public virtual PipelineVariableDictionary PipelineToolBindingValue { get; set; }
    }

    public abstract class PipelineToolBinding<TDTO> : IPipelineToolBinding where TDTO : JObject
    {
        public PipelineToolBinding()
        { }

        public virtual string PipelineToolBindingDisplayName { get; set; }
        public virtual string PipelineToolBindingKey { get; set; }
        public virtual PipelineVariableDictionary PipelineToolBindingValue { get; set; }
    }
}
