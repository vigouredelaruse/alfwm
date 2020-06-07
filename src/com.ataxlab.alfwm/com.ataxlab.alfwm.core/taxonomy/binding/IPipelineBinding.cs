using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    /// <summary>
    /// should serve most needs where one is tempted to go for a generic definition
    /// since PipelineVariableDictionary is designed for weak typing
    /// </summary>
    public interface IPipelineBinding   {

        string PipelineBindingDisplayName { get; set; }
        string PipelineBindingKey { get; set; }
        PipelineVariableDictionary PipelineBindingValue { get; set; }

    }

    /// <summary>
    /// implementation needs to arrange for 
    /// parsing the JObject graph 
    /// extracting the properties and their values
    /// and population of 
    /// 
    /// IPipelineBinding.Value &&
    /// IPipelineBinding.Key
    /// </summary>
    /// <typeparam name="TDTO"></typeparam>
    public interface IPipelineBinding<TDTO> : IPipelineBinding where TDTO : JObject
    {

    }
}
