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
    public interface IPipelineDataSetProvider<TPersistenceProviderConfig, TConfigureResult> : IPersistenceProvider<TPersistenceProviderConfig, TConfigureResult>
                                                where TPersistenceProviderConfig : class, new()
                                                where TConfigureResult : class, new()
    {  
         
    }

}
