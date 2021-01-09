using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model
{

    public class TaskItemPipelineVariable : PipelineVariable<TaskItem, List<TaskItem>, TaskItemPipelineVariable>, ICloneable, IPipelineToolConfiguration
    {
        public TaskItemPipelineVariable()
        {
            this.PipelineVariables = new List<PipelineVariable>();
        }

        /// <summary>
        /// test entity
        /// concrete implementation of IPipelineVariable<>
        /// </summary>
        /// <param name="payload"></param>
        public TaskItemPipelineVariable(TaskItem payload) : base(payload)
        {
            this.PipelineVariables = new List<PipelineVariable>();
        }

        public List<PipelineVariable> PipelineVariables { get; set;}
        string IPipelineToolConfiguration.Id { get; set; }
        string IPipelineToolConfiguration.Key { get; set; }
        string IPipelineToolConfiguration.DisplayName { get; set; }
        object IPipelineToolConfiguration.Configuration { get; set; }
        DateTime IPipelineToolConfiguration.TimeStamp { get; set; }
        string IPipelineToolConfiguration.ConfigurationJson { get; set; }
        string IPipelineToolConfiguration.ConfigurationJsonSchema { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
