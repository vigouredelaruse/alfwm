using com.ataxlab.alfwm.core.persistence;
using com.ataxlab.alfwm.core.taxonomy.binding;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.grammar.verbs
{
    public interface IPipelineToolDataSetProvider<T> : IPersistenceProvider, IPipelineToolBinding<T> 
        where T : JObject
    {
    }
}
