using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    public abstract class PipelineBinding : IPipelineBinding
    {
        public abstract string PipelineBindingDisplayName { get; set; }
        public abstract string PipelineBindingKey { get; set; }
        public abstract PipelineVariableDictionary PipelineBindingValue { get; set; }
    }

    public abstract class PipelineBinding<TDTO> : IPipelineBinding<TDTO> where TDTO : JObject
    {
        public abstract string PipelineBindingDisplayName { get; set; }
        public abstract string PipelineBindingKey { get; set; }
        public abstract PipelineVariableDictionary PipelineBindingValue { get; set; }
    }

}
