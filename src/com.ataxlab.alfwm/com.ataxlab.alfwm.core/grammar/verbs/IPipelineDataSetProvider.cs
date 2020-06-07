using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.core.taxonomy.binding;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
namespace com.ataxlab.alfwm.core.grammar.verbs
{

    /// <summary>
    /// a pipeline dataset provider is a persistence provider
    /// that exposes a pipeline binding biased for weak typing
    /// </summary>
    public interface IPipelineDataSetProvider : IPersistenceProvider, IPipelineBinding
    {  
         
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPipelineDataSetProvider<T> : IPersistenceProvider, IPipelineBinding<T>
        where T : JObject
    {
    }
}
