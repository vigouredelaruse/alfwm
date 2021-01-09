using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// provides a 'well-known' object container
    /// suitable for use in pipeline scenarios
    /// where dynamic type binding is necessary
    /// </summary>
    public class PipelineToolConfiguration : IPipelineToolConfiguration
    {
        public PipelineToolConfiguration()
        {
            PipelineVariables = new List<PipelineVariable>();
        }

        public string DisplayName {get; set; }
        public DateTime TimeStamp {get; set; }
        public string ConfigurationJson {get; set; }
        public string ConfigurationJsonSchema {get; set; }
        public string Id {get; set; }
        public string Key {get; set; }
        public object Configuration {get; set; }
        public List<PipelineVariable> PipelineVariables { get; set;}
    }

    public class PipelineToolConfiguration<TPayload> : IPipelineToolConfiguration<TPayload>, IPipelineToolConfiguration
 
    {

        public PipelineToolConfiguration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PipelineVariables = new List<PipelineVariable>();
        }

        public TPayload Payload { get; set;}
        public string DisplayName { get; set;}
        public DateTime TimeStamp { get; set;}
        public string ConfigurationJson { get; set;}
        public string ConfigurationJsonSchema { get; set;}
        public string Id {get; set; }
        public string Key {get; set; }
        public List<PipelineVariable> PipelineVariables { get; set;}
        object IPipelineToolConfiguration.Configuration {get; set; }
    }

}
