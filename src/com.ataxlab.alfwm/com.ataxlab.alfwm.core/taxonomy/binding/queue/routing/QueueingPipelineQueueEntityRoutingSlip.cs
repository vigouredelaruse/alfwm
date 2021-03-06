﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue.routing
{

    public class RuntimeHostAddress
    {
        public RuntimeHostAddress()
        { }

        public String RuntimeHostId { get; set; }

        public String RuntimeHostInstanceID { get; set; }
    }

    public class DeploymentContainerAddress
    {
        public DeploymentContainerAddress()
        {

        }

        public String ContainerId { get; set; }

        public String ContainerInstanceId { get; set; }
    }
    /// <summary>
    /// a process definition is deployed at this location
    /// that also encapsulates the process definition's pipeline
    /// </summary>
    public class DeploymentAddress
    {
        public DeploymentAddress()
        {

        }

        public string DeploymentId { get; set; }

        public string InstanceId { get; set; }
    }

    /// <summary>
    /// address a pipeline tool by its slot
    /// and combined pipeline IDs
    /// </summary>
    public class PipelineToolAddress
    {
        public PipelineToolAddress()
        {

        }

        /// <summary>
        /// tolerant of nulls here
        /// </summary>
        public string PipelineInstanceId { get; set; }
        public string PipelineId { get; set; }
        public int PipelineToolSlot { get; set; }
    }

    /// <summary>
    /// analog of an IP address or MAC address
    /// that are multi-segment compound references
    /// that specifies the locations of Queueing Pipeline 
    /// routeable locations
    /// </summary>
    public class QueueingPipelineRoutingAddress
    {
        public QueueingPipelineRoutingAddress()
        {
            RuntimeHostAddress = new RuntimeHostAddress();
            PipelineToolAddress = new PipelineToolAddress();
            DeploymentAddress = new DeploymentAddress();
            DeploymentContainerAddress = new DeploymentContainerAddress();
        }

        /// <summary>
        /// references a container of entities
        /// that can be addressed by a deployment container address
        /// </summary>
        public RuntimeHostAddress RuntimeHostAddress { get; set; }

        /// <summary>
        /// references a container of entitites 
        /// that can be referenced by a deployment address
        /// </summary>
        public DeploymentContainerAddress DeploymentContainerAddress { get; set; }

        /// <summary>
        /// references a container of entities that can be address with a 
        /// PipelineToolAddress
        /// </summary>
        public DeploymentAddress DeploymentAddress { get; set; }


        /// <summary>
        /// references a container of entities that can be addressed with
        /// a compound pipelineid, and a pipeline tool slot
        /// </summary>
        public PipelineToolAddress PipelineToolAddress { get; set; }

    }

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

            SourceAddress = new QueueingPipelineRoutingAddress();
            DestinationAddress = new QueueingPipelineRoutingAddress();

            IsIgnoreRoutingSlipSteps = false;
        }

        public Boolean IsIgnoreRoutingSlipSteps { get; set; }

        public QueueingPipelineRoutingAddress SourceAddress { get; set; }

        public QueueingPipelineRoutingAddress DestinationAddress { get; set; }

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
            IsIgnoreRoutingSlip = false;
        }

        public Boolean IsIgnoreRoutingSlip { get; set; }

        /// <summary>
        /// returns a routing slip for a pipeline id and slot
        /// GIANT synchronization ANTI-PATTERN TODO
        /// move this and similar code to a builder that must be instantiated
        /// </summary>
        /// <param name="destinationPipelineId"></param>
        /// <param name="destinationSlot"></param>
        /// <returns></returns>
        public static QueueingPipelineQueueEntityRoutingSlipStep GetRoutingSlipStep(string destinationPipelineId, int destinationSlot)
        {
            return new QueueingPipelineQueueEntityRoutingSlipStep()
            {
                DestinationPipeline =
                                new Tuple<QueueingPipelineRoutingSlipDestination, string>(QueueingPipelineRoutingSlipDestination.Pipeline, destinationPipelineId),
                DestinationSlot = new Tuple<QueueingPipelineRoutingSlipDestination, int>(QueueingPipelineRoutingSlipDestination.PipelineSlot, destinationSlot)
            };
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
