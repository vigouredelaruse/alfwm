using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// support a serialiazable config
    /// </summary>
    [Obsolete]
    public interface IPipelineToolConfiguration
    {

        string DisplayName { get; set; }

        DateTime DeploymentTime { get; set; }

        string ConfigurationJson { get; set; }

        /// <summary>
        /// the expectation is that the implemenation layer of the workflow service
        /// is decoupled from the clients of public interface of the workflow service
        /// 
        /// the implementation layer can identify the specifics that can be easily 
        /// changed with a schema update
        /// </summary>
        string ConfigurationJsonSchema { get; set; }
    }

    /// <summary>
    /// support generic semantics
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public interface IPipelineToolConfiguration<TConfiguration> : IPipelineToolConfiguration
    {
        TConfiguration Configuration { get; set; }
    }
}
