using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IPipelineStatus
    {
        string StatusJson { get; set; }

        string StatusJsonSchema { get; set; }
    }

    /// <summary>
    /// support generic symantics for 
    /// implementation specific pipelines
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPipelineStatus<T> where T : class
    {
        T Payload { get; set; }
    }
}
