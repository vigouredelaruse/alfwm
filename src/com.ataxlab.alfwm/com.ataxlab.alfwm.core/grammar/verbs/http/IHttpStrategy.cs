using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace com.ataxlab.alfwm.core.grammar.verbs.http
{
    public interface IHttpStrategy<TResult, TParams> 
                                    where TResult : JObject
                                    where TParams : HttpStrategyRequestMessage
    {


        TResult Get(TParams parameters);
        TResult Head(TParams parameters);

        TResult Post(TParams parameters);
        TResult Put(TParams parameters);
        TResult Delete(TParams parameters);
        TResult Patch(TParams parameters);
    }
}
