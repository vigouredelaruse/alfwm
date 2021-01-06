﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{

    public class DefaultPipelineNodeQueueingPipeline : IQueueingPipeline
    {
        public DefaultPipelineNodeQueueingPipeline()
        {
            this.ProcessDefinition = new DefaultQueueingPipelineProcessDefinition();
        }

        public string PipelineId { get; set; }
        public string PipelineInstanceId { get; set; }
        public string PipelineDisplayName { get; set; }
        public string PipelineDescription { get; set; }
        public IPipelineBinding PipelineInputBinding { get; set; }
        public IPipelineBinding PipelineOutputBinding { get; set; }
        public IDefaultQueueingPipelineProcessDefinition ProcessDefinition { get; set; }

        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public bool Bind(string SourceInstanceId, string DestinationInstanceId)
        {
            // try to wire the input channel of the destination
            // to the output binding collection of the source
            return false;
        }

        /// <summary>
        /// linked list semantics for adding pipeline node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddLastPipelineNode(QueueingPipelineNode newNode)
        {

            this.ProcessDefinition.QueueingPipelineNodes.AddLast(newNode);

            return true;
        }

        public bool AddFirstPipelineNode(QueueingPipelineNode newNode)
        {
            
            this.ProcessDefinition.QueueingPipelineNodes.AddFirst(newNode);

            return true;
        }



        public bool AddAfterPipelineNode(int pipelineNodeIndex, QueueingPipelineNode newNode)
        {
            // find the node by its id
            IQueueingPipelineNode targetNode = this.ProcessDefinition.QueueingPipelineNodes.Skip<IQueueingPipelineNode>(pipelineNodeIndex).Take(1).FirstOrDefault();
         
            if (targetNode != null)
            {
                //newNode.QueueingPipelineTool.QueueingInputBinding
                targetNode.QueueingPipelineTool.QueueingOutputBindingCollection.Add(
                    newNode.QueueingPipelineTool.QueueingInputBinding
                    );
                // associate the target with its LinkedListNode container
                var targetContainer = this.ProcessDefinition.QueueingPipelineNodes.Find(targetNode);

                // bind new downstream node if necessary
                // TODO discover if this node is already wired somewhere else
                // foreach(var node in ProcessDefinitionLinkedList)
                // {
                //      // as per https://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
                //      node.QueueingOutputBidingCollection.RemoveAll(item => item.Id == newNode.QueueingInputBinding.Id);
                //
                //  }
                if (targetContainer?.Next != null)
                {
                    // enforce nose-to-tail linked list binding
                    var downstreamNode = targetContainer.Next;

                    newNode.PipelineTool.QueueingOutputBindingCollection.Add(
                        new QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>>()
                        );
                }

                this.ProcessDefinition.QueueingPipelineNodes.AddAfter(targetContainer, newNode);

            }
            else
            {
                return false;
            }

            return true;
        }

        public void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            PipelineCompleted?.Invoke(sender, args);
        }

        public void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            PipelineFailed?.Invoke(sender, args);
        }

        public void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            PipelineProgressUpdated?.Invoke(sender, args);
        }

        public void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            PipelineStarted?.Invoke(sender, args);
        }

        public void StartPipeline(IDefaultQueueingPipelineProcessDefinition configuration)
        {
        }

        public void StopPipeline(string instanceId)
        {
            
        }
    }


    public class PipelineNodeQueueingPipelineEx : PipelineNodeQueueingPipelineBaseEx
        <
                    IQueueingPipelineTool<
                QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration>,
            IQueueingPipelineTool<
                QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration>,
            QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
            QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
            IPipelineToolConfiguration,
            IPipelineToolConfiguration,
            IPipelineToolConfiguration
        >



    {

        public PipelineNodeQueueingPipelineEx()
        {
            this.ProcessDefinition = new QueueingPipelineProcessDefinitionEx<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>, IPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration>();

        }

        public override QueueingPipelineProcessDefinitionEx<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>, IPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration> ProcessDefinition { get; set; }
        public override string PipelineId { get; set; }
        public override string PipelineInstanceId { get; set; }
        public override string PipelineDisplayName { get; set; }
        public override string PipelineDescription { get; set; }
        public override IPipelineBinding PipelineInputBinding { get; set; }
        public override IPipelineBinding PipelineOutputBinding { get; set; }

        public override event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public override event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public override event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public override event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public override string AddTool(IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration> node)
        {
            int i = 0;
            return "";
        }

        public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            
        }

        public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            
        }

        public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            
        }

        public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            
        }

        public override void StartPipeline(QueueingPipelineProcessDefinitionEx<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>, IPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration> configuration)
        {
            
        }

        public override void StopPipeline(string instanceId)
        {
            
        }
    }


    public class PipelineNodeQueueingPipeline : PipelineNodeQueueingPipelineBase
    {
        public override string PipelineId { get; set; }
        public override string PipelineInstanceId { get; set; }
        public override string PipelineDisplayName { get; set; }
        public override string PipelineDescription { get; set; }
        public override IPipelineBinding PipelineInputBinding { get; set; }
        public override IPipelineBinding PipelineOutputBinding { get; set; }

        public override event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public override event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public override event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public override event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public override string AddTool(QueueingPipelineNode<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>> node)
        {
            return string.Empty;
        }

        public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            
        }

        public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            
        }

        public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            
        }

        public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            
        }

        public override void StartPipeline(QueueingPipelineProcessDefinition<QueueingPipelineNode<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>>> configuration)
        {
            
        }

        public override void StopPipeline(string instanceId)
        {
            
        }
    }

    //public class PipelineNodeQueueingPipeline<TProcessDefinition,
    //                                            TPipelineTool,
    //                                            TLatchingInputBinding,
    //                                            TOutputBinding,
    //                                            TPipelineToolConfiguration,
    //                                            TInputEntity,
    //                                            TOutputEntity
    //                                            > : PipelineNodeQueueingPipelineBase
    //    where TProcessDefinition : class, IQueueingPipelineProcessDefinition<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity>, new()
    //    where TPipelineTool :  IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration >
    //    where TLatchingInputBinding : class, IQueueConsumerPipelineToolBinding<TInputEntity>,  new()
    //    where TOutputBinding : class, IQueueProducerPipelineToolBinding<TOutputEntity>, new()
    //    where TPipelineToolConfiguration : class, IPipelineToolConfiguration, new()
    //    where TInputEntity : class, new()
    //    where TOutputEntity : class, new()
    //{
    //    public override TProcessDefinition ProcessDefinition { get; set; }
    //    public override string PipelineId { get; set; }
    //    public override string PipelineInstanceId { get; set; }
    //    public override string PipelineDisplayName { get; set; }
    //    public override string PipelineDescription { get; set; }
    //    public override IPipelineBinding PipelineInputBinding { get; set; }
    //    public override IPipelineBinding PipelineOutputBinding { get; set; }

    //    public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
    //    public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
    //    public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
    //    public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

    //    public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void StartPipeline(TProcessDefinition configuration)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void StopPipeline(string instanceId)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
