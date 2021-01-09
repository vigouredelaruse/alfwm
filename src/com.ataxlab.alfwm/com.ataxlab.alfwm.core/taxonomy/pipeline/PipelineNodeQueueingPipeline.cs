using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// canonical implementation of a Queueing Pipeline
    /// 
    /// treat as UserControl from the WPF world, a convenience implementation
    /// of the dependencies captured by IQueueingPipeline
    /// </summary>
    public class DefaultPipelineNodeQueueingPipeline : IDefaultQueueingPipeline
    {
        public DefaultPipelineNodeQueueingPipeline()
        {
            this.ProcessDefinition = new DefaultQueueingPipelineProcessDefinition();
            this.PipelineId = Guid.NewGuid().ToString();
            this.PipelineDisplayName = this.GetType().Name;
            this.PipelineInstanceId = Guid.NewGuid().ToString();


        }


        public string PipelineId { get; set; }
        public string PipelineInstanceId { get; set; }
        public string PipelineDisplayName { get; set; }
        public string PipelineDescription { get; set; }
        public IPipelineBinding PipelineInputBinding { get; set; }
        public IPipelineBinding PipelineOutputBinding { get; set; }

        /// <summary>
        /// exposes the input binding of the ingress pipelinetool 
        /// functions as the main 'entry point' for messages 
        /// to the pipeline
        /// </summary>
        public QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingInputBinding { get; set; }

        /// <summary>
        /// egresses messages from the terminal node of the pipeline
        /// </summary>
        public IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingOutputBinding { get; set; }

        public IDefaultQueueingPipelineProcessDefinition ProcessDefinition { get; set; }

        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;
        public event EventHandler<PipelineDeploymentFailedEventArgs> PipelineDeploymentFailed;

        /// <summary>
        /// deploy a process definition
        /// TODO instrument the result, perhaps via deployment error events
        /// </summary>
        /// <param name="processDefinition"></param>
        public void Deploy(DefaultQueueingPipelineProcessDefiniionEntity processDefinition)
        {
            // clear the process definition
            // TODO - stop the tools first
            this.ProcessDefinition.QueueingPipelineNodes.Clear();


            try
            {
                var nodes = processDefinition.QueueingPipelineNodes.OrderBy(o => o.ToolChainSlotNumber).ToList<QueueingPipelineNodeEntity>();

                foreach (var node in nodes)
                {
                    // instantiate the node
                    // TODO engineer management of Ids upon and after instantiation
                    Type t = Type.GetType(node.ClassName);
                    var newNode = (QueueingPipelineToolNode)Activator.CreateInstance(t);

                    // instantiate the pipeline tool
                    Type tTool = Type.GetType(node.QueueingPipelineTool.QueueingPipelineToolClassName);
                    var newTool = Activator.CreateInstance(tTool);

                    newNode.QueueingPipelineTool = (IDefaultQueueingPipelineTool)newTool;


                    // instantiate the pipeline tool's pipeline variables
                    foreach (var item in node.QueueingPipelineTool.PipelineVariables)
                    {

                        ((IDefaultQueueingPipelineTool)newTool).PipelineToolVariables.Add(item);
                    }

                    // add the node to the process definition
                    if (node.ToolChainSlotNumber == 0)
                    {
                        // the first node gets special treatment
                        var result = this.AddFirstPipelineNode(newNode);

                    }
                    else
                    {
                        var result = this.AddAfterPipelineNode(node.ToolChainSlotNumber - 1, newNode);
                    }
                }

            }
            catch (Exception e)
            {
                var args = new PipelineDeploymentFailedEventArgs()
                { DeploymentFailureException = e };

                OnPipelineDeploymentFailed(this, args);

            }
        }


        /// <summary>
        /// enforce exposure inputbinding queue of the ingress pipelinetool node
        /// 
        /// </summary>
        public void EnsurePipelineIngressEgressBindings()
        {
            // apply the rule that the first ordinal node in the 
            // process definition list is the ingress node
            var ingressNode = this.ProcessDefinition.QueueingPipelineNodes.First;

            this.QueueingInputBinding = ingressNode.Value.QueueingPipelineTool.QueueingInputBinding;

            var egressNode = this.ProcessDefinition.QueueingPipelineNodes.Last;

            this.QueueingOutputBinding = egressNode.Value.QueueingPipelineTool.QueueingOutputBinding;
            // TODO instrument telemetry for this operation
        }

        /// <summary>
        /// linked list semantics for adding pipeline node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddLastPipelineNode(QueueingPipelineToolNode newNode)
        {
            EnsurePipelineToolListeners(newNode);

            this.ProcessDefinition.QueueingPipelineNodes.AddLast(newNode);
            EnsurePipelineIngressEgressBindings();
            return true;
        }

        /// <summary>
        /// wire the pipeline to the added tool's events
        /// </summary>
        /// <param name="newNode"></param>
        public void EnsurePipelineToolListeners(QueueingPipelineToolNode newNode)
        {
            newNode.QueueingPipelineTool.PipelineToolStarted += QueueingPipelineTool_PipelineToolStarted;
            newNode.QueueingPipelineTool.PipelineToolCompleted += QueueingPipelineTool_PipelineToolCompleted;
            newNode.QueueingPipelineTool.PipelineToolProgressUpdated += QueueingPipelineTool_PipelineToolProgressUpdated;
            newNode.QueueingPipelineTool.PipelineToolFailed += QueueingPipelineTool_PipelineToolFailed;
        }

        private void QueueingPipelineTool_PipelineToolFailed(object sender, PipelineToolFailedEventArgs e)
        {
            PipelineFailed?.Invoke(sender, new PipelineFailedEventArgs() { ToolFailedEvent = e });
        }

        private void QueueingPipelineTool_PipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs e)
        {
            PipelineProgressUpdated?.Invoke(sender, new PipelineProgressUpdatedEventArgs() { ToolProgressUpdatedEvent = e });

        }

        private void QueueingPipelineTool_PipelineToolCompleted(object sender, PipelineToolCompletedEventArgs e)
        {
            PipelineProgressUpdated?.Invoke(sender, new PipelineProgressUpdatedEventArgs() { ToolCompletedEvent = e });

        }

        private void QueueingPipelineTool_PipelineToolStarted(object sender, PipelineToolStartEventArgs e)
        {
            PipelineProgressUpdated?.Invoke(sender, new PipelineProgressUpdatedEventArgs() { ToolStartedEvent = e });
        }

        public bool AddFirstPipelineNode(QueueingPipelineToolNode newNode)
        {
            EnsurePipelineToolListeners(newNode);
            this.ProcessDefinition.QueueingPipelineNodes.AddFirst(newNode);
            EnsurePipelineIngressEgressBindings();
            return true;
        }



        public bool AddAfterPipelineNode(int pipelineNodeIndex, QueueingPipelineToolNode newNode)
        {

            EnsurePipelineToolListeners(newNode);
            // find the node by its id
            QueueingPipelineToolNode targetNode = this.ProcessDefinition.QueueingPipelineNodes.Skip<QueueingPipelineToolNode>(pipelineNodeIndex).Take(1).FirstOrDefault();

            if (targetNode != null)
            {
                // wire the new node's input to it's predecessor's output
                targetNode.QueueingPipelineTool.QueueingOutputBindingCollection.Add(
                    newNode.QueueingPipelineTool.QueueingInputBinding
                    );

                // wire the new node's output to it's successor's input - don't break the chain
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
                    newNode.QueueingPipelineTool.QueueingOutputBindingCollection.Add(targetContainer.Value.QueueingPipelineTool.QueueingInputBinding);
                }

                this.ProcessDefinition.QueueingPipelineNodes.AddAfter(targetContainer, newNode);

            }
            else
            {
                EnsurePipelineIngressEgressBindings();
                return false;
            }

            EnsurePipelineIngressEgressBindings();
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

        public void OnPipelineDeploymentFailed(object sender, PipelineDeploymentFailedEventArgs args)
        {
            PipelineDeploymentFailed?.Invoke(sender, args);
        }
    }
}
