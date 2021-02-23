using com.ataxlab.alfwm.core.taxonomy.binding.queue.routing;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.utility.extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    public interface IQueueingPipelineQueueEntity<TEntity>
        where TEntity : IPipelineToolConfiguration
    {
        TEntity Payload { get; set; }

        #region envelope addressing

        /// <summary>
        /// application of the routing slip pattern
        /// </summary>
        QueueingPipelineQueueEntityRoutingSlip RoutingSlip { get; set; }

        string CurrentPipelineId { get; set; }

        /// <summary>
        /// support propagation of variables in scope
        /// </summary>
        /// <param name="sourceEntity"></param>
        void PopulatePipelineVariables(TEntity sourceEntity);
        #endregion
    }

    /// <summary>
    /// furnish a consistent type for queue entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class QueueingPipelineQueueEntity<TEntity> : IPipelineToolConfiguration, IQueueingPipelineQueueEntity<TEntity>
        where TEntity : IPipelineToolConfiguration
    {

        public QueueingPipelineQueueEntity(TEntity payload) : this()
        {

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

        public string CurrentPipelineId { get; set; }
        public QueueingPipelineQueueEntityRoutingSlip RoutingSlip { get; set; }

        /// <summary>
        /// support propagation of pipeline variable scope
        /// </summary>
        /// <param name="sourceEntity"></param>
        public void PopulatePipelineVariables(TEntity sourceEntity)
        {
            foreach (PipelineVariable pipelineVariable in sourceEntity.PipelineVariables)
            {
                this.PipelineVariables.Add(pipelineVariable);
            }
        }
    }
}
