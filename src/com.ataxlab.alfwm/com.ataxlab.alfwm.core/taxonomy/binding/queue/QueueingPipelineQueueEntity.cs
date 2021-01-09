using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    public interface IQueueingPipelineQueueEntity<TEntity>
        where TEntity : IPipelineToolConfiguration
    {
        TEntity Payload { get; set; }
    }

    /// <summary>
    /// furnish a consistent type for queue entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class QueueingPipelineQueueEntity<TEntity> : IPipelineToolConfiguration, IQueueingPipelineQueueEntity<TEntity>
        where TEntity : IPipelineToolConfiguration
    {

        public QueueingPipelineQueueEntity(TEntity payload) : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Payload = payload;
        }

        public QueueingPipelineQueueEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            TimeStamp = DateTime.UtcNow;
            this.PipelineVariables = new List<PipelineVariable>();
        }

        public string Id { get; set; }
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public object Configuration { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ConfigurationJson { get; set; }
        public string ConfigurationJsonSchema { get; set; }
        public TEntity Payload { get; set; }
        public List<PipelineVariable> PipelineVariables { get; set;}
    }
}
