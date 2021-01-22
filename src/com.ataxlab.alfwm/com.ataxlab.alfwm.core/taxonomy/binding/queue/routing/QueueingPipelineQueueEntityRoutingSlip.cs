using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue.routing
{


    /// <summary>
    /// application of routing slip pattern
    /// 
    /// pipelines and queues and queue slots are 
    /// selected for message destinations via this routing slip
    /// </summary>
    public class QueueingPipelineQueueEntityRoutingSlip
    {

        public QueueingPipelineQueueEntityRoutingSlip()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;

            RoutingSteps = new LinkedList<QueueingPipelineQueueEntityRoutingSlipStep>();
            VisitedPipelines = new ConcurrentStack<string>();
        }

        public String Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public LinkedList<QueueingPipelineQueueEntityRoutingSlipStep> RoutingSteps { get; private set; }

        /// <summary>
        /// the seenby list 
        /// useful for dead-lettering
        /// </summary>
        public ConcurrentStack<String> VisitedPipelines { get; set; }
    }

    /// <summary>
    /// encapsulates a routling slip step
    /// </summary>
    public class QueueingPipelineQueueEntityRoutingSlipStep
    {
        /// <summary>
        /// defer cardinality of the step to other concerns
        /// </summary>
        public QueueingPipelineQueueEntityRoutingSlipStep()
        {

        }

        /// <summary>
        /// TODO - modify setters to enforce the QueueingPipelineRoutingSlipDestination for a given property 
        /// </summary>
        public Tuple<QueueingPipelineRoutingSlipDestination, String> DestinationRuntimehost { get; set; }
        public Tuple<QueueingPipelineRoutingSlipDestination, String> DestinationContainer { get; set; }

        public Tuple<QueueingPipelineRoutingSlipDestination, String> DestinationDeployment { get; set; }

        public Tuple<QueueingPipelineRoutingSlipDestination, String> DestinationPipeline { get; set; }

        public Tuple<QueueingPipelineRoutingSlipDestination, int> DestinationSlot { get; set; }
    }

    /// <summary>
    /// encapsulates a routing step destination
    /// </summary>
    public enum QueueingPipelineRoutingSlipDestination { RuntimeHost, Container, Deployment, Pipeline, PipelineSlot };


}
