using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IPipelineToolStatus
    {
        string StatusJson { get; set; }

        string StatusJsonSchema { get; set; }
    }

    /// <summary>
    /// support generic semantics 
    /// leading to return types specific to pipeline tool implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPipelineToolStatus<T> where T : class
    {
        T Payload { get; set; }
    }
}
